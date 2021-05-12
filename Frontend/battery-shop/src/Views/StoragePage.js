import React, { useState, useEffect } from "react";

import { useHistory } from "react-router-dom";

import { Table, message } from "antd";

import { getStorages } from "../utils";

export default function StoragePage() {
  const [storages, setStorages] = useState([]);
  const [loading, setLoading] = useState(true);

  const history = useHistory();

  const columns = [
    {
      title: "Id",
      dataIndex: "id",
    },
    {
      title: "Name",
      dataIndex: "name",
    },
  ];

  useEffect(() => {
    (async () => {
      try {
        const { data } = await getStorages();

        setStorages(data);
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
        title={() => "Storages"}
        rowKey="id"
        loading={loading}
        dataSource={storages}
        columns={columns}
        onRow={(record) => {
          return {
            onClick: () => history.push(`storage/${record.id}`),
          };
        }}
      />
    </div>
  );
}
