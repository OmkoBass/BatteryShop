import React, { useState, useContext } from "react";

import {
  Modal,
  Button,
  Form,
  Input,
  InputNumber,
  message,
  notification,
} from "antd";

import { AuthContext } from "../Auth";

import { addBattery } from "../utils";

export default function AddBatteryModal({
  visible,
  onClose,
  storageId,
  addBatteryToTable,
}) {
  const [loading, setLoading] = useState(false);

  const { batteryShop } = useContext(AuthContext);

  const handleOnFinish = (values) => {
    (async () => {
      setLoading(true);
      try {
        values.storageId = storageId;
        values.batteryShopId = batteryShop.id;
        const { data } = await addBattery(values);

        notification.success({
          message: "Battery added!",
          description: `Battery added to this storage!`,
        });

        addBatteryToTable(data);
        onClose();
      } catch {
        message.error("Something went wrong!");
      } finally {
        setLoading(false);
      }
    })();
  };

  return (
    <Modal
      title="Add Battery"
      visible={visible}
      footer={false}
      closable={false}
    >
      <Form onFinish={handleOnFinish}>
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
              max: 32,
              message: "Maximum 32 charachters!",
            },
          ]}
        >
          <Input size="large" placeholder="Battery Name" />
        </Form.Item>

        <Form.Item
          name="price"
          rules={[
            {
              required: true,
              message: "Last Name is required!",
            },
          ]}
        >
          <InputNumber size="large" min={0} placeholder="Price" />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Add
          </Button>

          <Button
            type="danger"
            onClick={() => onClose()}
            style={{ marginLeft: "1em" }}
          >
            Close
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
}
