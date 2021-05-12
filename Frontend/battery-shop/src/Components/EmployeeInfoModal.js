import React, { useState } from "react";

import { Modal, Descriptions, Button, notification, message } from "antd";

import { deleteEmployee, handleDisplayProperJob } from "../utils";

export default function EmployeeInfoModal({
  visible,
  employee,
  handleClose,
  removeEmployeeFromTable,
}) {
  const [loading, setLoading] = useState(false);

  const handleDeleteEmployee = async (id) => {
    setLoading(true);
    try {
      await deleteEmployee(id);

      notification.success({
        message: `${employee.username} deleted`,
        description: `Employee with the username: ${employee.username} has been deleted.`,
      });

      removeEmployeeFromTable(id);

      handleClose();
    } catch {
      message.error("Something went wrong!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Modal
      visible={visible}
      onCancel={handleClose}
      title={employee?.username}
      footer={false}
    >
      <Descriptions title="Employee info" bordered layout="vertical">
        <Descriptions.Item label="Username">
          {" "}
          {employee?.username}{" "}
        </Descriptions.Item>
        <Descriptions.Item label="Name"> {employee?.name} </Descriptions.Item>
        <Descriptions.Item label="Last Name">
          {" "}
          {employee?.lastName}{" "}
        </Descriptions.Item>
        <Descriptions.Item label="Job">
          {" "}
          {handleDisplayProperJob(employee?.job)}{" "}
        </Descriptions.Item>
      </Descriptions>

      <div style={{ marginTop: "1em" }}>
        <Button type="primary" onClick={handleClose} loading={loading}>
          Close
        </Button>
        <Button
          style={{ marginLeft: "1em" }}
          type="danger"
          onClick={() => handleDeleteEmployee(employee.id)}
          loading={loading}
        >
          Delete
        </Button>
      </div>
    </Modal>
  );
}
