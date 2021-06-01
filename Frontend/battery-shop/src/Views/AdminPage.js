import React, { useState, useEffect, useContext } from "react";

import { Table, Typography, message, Input, Button } from "antd";

import { useHistory } from "react-router-dom";

import { getBatteryShopEmployees, handleDisplayProperJob } from "../utils";

import { AuthContext } from "../Auth";

import EmployeeInfoModal from "../Components/EmployeeInfoModal";
import AddEmployeeModal from "../Components/AddEmployeeModal";

export default function AdminPage() {
  const [loading, setLoading] = useState(true);
  const [employees, setEmployees] = useState([]);
  const [employeeInfoVisible, setEmployeeInfoVisible] = useState(false);
  const [employeeInfo, setEmployeeInfo] = useState(null);
  const [searchedEmployeeInfo, setSearchedEmployeeInfo] = useState([]);
  const [addEmployeeVisible, setAddEmployeeVisible] = useState(false);

  const history = useHistory();

  const { employee } = useContext(AuthContext);

  useEffect(() => {
    if (employee?.job !== 4) {
      history.push("/");
    } else {
      (async () => {
        try {
          const { data } = await getBatteryShopEmployees();

          setEmployees(data);
        } catch {
          message.error("Something went wrong!");
        } finally {
          setLoading(false);
        }
      })();
    }
  }, [employee?.job, history]);

  const columns = [
    {
      title: "Id",
      dataIndex: "id",
    },
    {
      title: "Name",
      dataIndex: "name",
    },
    {
      title: "Lastname",
      dataIndex: "lastName",
    },
    {
      title: "Username",
      dataIndex: "username",
    },
    {
      title: "Job",
      dataIndex: "job",
      render: (jobId) => (
        <Typography.Text>{handleDisplayProperJob(jobId)}</Typography.Text>
      ),
    },
  ];

  return (
    <div>
      <Typography.Title level={4}>
        You are the admin, you can add and delete employees. You can also view your customers.
      </Typography.Title>

      <Button type="primary" onClick={() => setAddEmployeeVisible(true)}>
        Add Employee
      </Button>
      <Input.Search
        style={{ marginTop: "1em" }}
        placeholder={"Enter name of the employee"}
        allowClear
        onSearch={(value) => {
          if (value) {
            setSearchedEmployeeInfo(
              employees.filter((employee) => employee.name.includes(value))
            );
          } else {
            setSearchedEmployeeInfo([]);
          }
        }}
      />
      <Table
        style={{ marginTop: "1em" }}
        title={() => "Employees"}
        loading={loading}
        dataSource={
          searchedEmployeeInfo.length === 0 ? employees : searchedEmployeeInfo
        }
        columns={columns}
        rowKey="id"
        onRow={(record) => {
          return {
            onClick: () => {
              setEmployeeInfoVisible(true);
              setEmployeeInfo(record);
            },
          };
        }}
      />

      <EmployeeInfoModal
        removeEmployeeFromTable={(id) =>
          setEmployees(employees.filter((employee) => employee.id !== id))
        }
        visible={employeeInfoVisible}
        employee={employeeInfo}
        handleClose={() => setEmployeeInfoVisible(false)}
      />

      <AddEmployeeModal
        visible={addEmployeeVisible}
        handleClose={() => setAddEmployeeVisible(false)}
        addEmployeeToTable={(employee) =>
          setEmployees([...employees, employee])
        }
      />
    </div>
  );
}
