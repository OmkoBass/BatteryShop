import React, { useState, useEffect } from "react";

import { useHistory } from "react-router-dom";

import {Table, message, Typography, Button} from "antd";

import { getStorages } from "../utils";
import StorageModal from "../Components/StorageModal";

export default function StoragePage() {
  const [storages, setStorages] = useState([]);
  const [loading, setLoading] = useState(true);
  const [storageModal, setStorageModal] = useState(false);

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
      <Typography.Title level={4}>
        Select a storage, view it's batteries and Add or Delete them.
      </Typography.Title>

      <Button type={'primary'}
              onClick={() => setStorageModal(true)}
      >
        Add Storage
      </Button>

      <Table
        style={{ marginTop: "1em" }}
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

        <StorageModal
            visible={storageModal}
            handleClose={() => setStorageModal(false)}
            addStorageToTable={storage => setStorages([...storages, storage])}
        />
    </div>
  );
}
