import React, { useState, useEffect } from "react";

import { Table, Typography, message } from "antd";

import { getBatteryShopEmployees } from "../utils";

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

const handleDisplayProperJob = (jobId) => {
  if (jobId === 4) {
    return "Admin";
  } else if (jobId === 3) {
    return "Intervetion";
  } else if (jobId === 2) {
    return "Supply";
  } else if (jobId === 1) {
    return "Sales";
  } else {
    return "Service";
  }
};

export default function AdminPage() {
  const [loading, setLoading] = useState(true);
  const [employees, setEmployees] = useState([]);

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

  return (
    <div>
      <Table
        title={() => "Employees"}
        loading={loading}
        dataSource={employees}
        columns={columns}
        rowKey="name"
        onRow={(record) => {
          return {
            onClick: () => console.log(record),
          };
        }}
      />
    </div>
  );
}
