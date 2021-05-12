import React, { useState, useEffect } from "react";

import { Table, Typography, message, Input } from "antd";

import { getBatteryShopEmployees, handleDisplayProperJob } from "../utils";

import EmployeeInfoModal from "../Components/EmployeeInfoModal";

export default function AdminPage() {
  const [loading, setLoading] = useState(true);
  const [employees, setEmployees] = useState([]);
  const [employeeInfoVisible, setEmployeeInfoVisible] = useState(false);
  const [employeeInfo, setEmployeeInfo] = useState(null);
  const [searchedEmployeeInfo, setSearchedEmployeeInfo] = useState([]);

  useEffect(() => {
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
  }, []);

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
      <Input.Search
        placeholder={"Enter name of the employee"}
        allowClear
        onSearch={(value) => {
          if (value) {
            setSearchedEmployeeInfo(
              employees.filter((employee) => employee.name === value)
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
        rowKey="name"
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
        visible={employeeInfoVisible}
        employee={employeeInfo}
        handleClose={() => setEmployeeInfoVisible(false)}
      />
    </div>
  );
}
