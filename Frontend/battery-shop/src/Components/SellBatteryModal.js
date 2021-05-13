import React, { useState } from 'react';

import {Modal, Button, Form, Input, message, notification } from "antd";

import { sellBattery } from "../utils";

export default function SellBatteryModal({ visible, handleClose, battery, updateSoldToTable }) {
    const [loading, setLoading] = useState(false);

    const handleOnFinish = async (values) => {
        setLoading(true);

        try {
            const { data } = await sellBattery(values, battery.id);

            notification.success({
                message: "Battery sold!",
                description: `Battery ${battery.name} sold!`,
            });

            handleClose();
            updateSoldToTable(data);
        } catch(ex) {
            console.log(ex);
            message.error('Something went wrong!');
        } finally {
            setLoading(false);
        }
    }

    return <Modal
        title={"Enter customer info"}
        visible={visible}
        onClose={handleClose}
        closable={false}
        footer={false}
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
                name="address"
                rules={[
                    {
                        required: true,
                        message: "Address is required!",
                    },
                    {
                        min: 3,
                        message: "Atleast 3 charachters!",
                    },
                    {
                        max: 16,
                        message: "Maximum 64 charachters!",
                    },
                ]}
            >
                <Input size="large" placeholder="Address" />
            </Form.Item>

            <Form.Item>
                <Button type="primary" htmlType="submit" loading={loading}>
                    Add
                </Button>

                <Button style={{ marginLeft: "1em" }} type={"danger"} onClick={() => handleClose()}>
                    Cancel
                </Button>
            </Form.Item>
        </Form>
    </Modal>
}
