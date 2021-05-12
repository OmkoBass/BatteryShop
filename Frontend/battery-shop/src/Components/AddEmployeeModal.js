import React, { useState } from "react";

import { addEmployee } from "../utils";

import {
  Modal,
  Divider,
  Button,
  Form,
  Input,
  Select,
  notification,
  message,
} from "antd";

export default function AddEmployeeModal({
  visible,
  handleClose,
  addEmployeeToTable,
}) {
  const [loading, setLoading] = useState(false);

  const handleOnFinish = async (value) => {
    setLoading(true);
    try {
      const { data } = await addEmployee(value);

      addEmployeeToTable(data);
      notification.success({
        message: "Employee added!",
        description: `Employee ${value.username} added!`,
      });
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
      title={"Add Employee"}
      footer={false}
      closable={false}
    >
      <Form onFinish={handleOnFinish}>
        <Form.Item
          name="username"
          rules={[
            {
              required: true,
              message: "Username is required!",
            },
            {
              min: 3,
              message: "Atleast 3 charachters!",
            },
            {
              max: 16,
              message: "Maximum 16 charachters!",
            },
          ]}
        >
          <Input size="large" placeholder="Username" />
        </Form.Item>

        <Form.Item
          name="password"
          rules={[
            {
              required: true,
              message: "Password is required!",
            },
            {
              min: 3,
              message: "Atleast 3 charachters!",
            },
            {
              max: 64,
              message: "Maximum 64 charachters!",
            },
          ]}
        >
          <Input.Password size="large" placeholder="Password" />
        </Form.Item>

        <Form.Item
          name="name"
          rules={[
            {
              required: true,
              message: "Name is required!",
            },
            {
              min: 3,
              message: "Atleast 3 charachters!",
            },
            {
              max: 16,
              message: "Maximum 16 charachters!",
            },
          ]}
        >
          <Input size="large" placeholder="Name" />
        </Form.Item>

        <Form.Item
          name="lastName"
          rules={[
            {
              required: true,
              message: "Last Name is required!",
            },
            {
              min: 3,
              message: "Atleast 3 charachters!",
            },
            {
              max: 32,
              message: "Maximum 32 charachters!",
            },
          ]}
        >
          <Input size="large" placeholder="Last Name" />
        </Form.Item>

        <Form.Item
          name="job"
          rules={[
            {
              required: true,
              message: "Job is required!",
            },
          ]}
        >
          <Select placeholder="Job">
            <Select.Option value="4">Admin</Select.Option>
            <Select.Option value="3">Intervetion</Select.Option>
            <Select.Option value="2">Supply</Select.Option>
            <Select.Option value="1">Sales</Select.Option>
            <Select.Option value="0">Service</Select.Option>
          </Select>
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Add
          </Button>
        </Form.Item>
      </Form>

      <Divider />

      <Button type="danger" onClick={() => handleClose()} loading={loading}>
        Cancel
      </Button>
    </Modal>
  );
}
