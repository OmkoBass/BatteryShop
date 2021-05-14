import React, { useState } from 'react';

import { createIntervention } from "../utils";

import {Modal, Form, Input, AutoComplete, Button, InputNumber, Typography, message, notification} from "antd";

export default function AddInterventionModal({ visible, batteries, updateSoldToTable, handleClose }) {
    const [battery, setBattery] = useState(null);
    const [loading, setLoading] = useState(false);

    const batteryNames = batteries.map(battery => {
        return {
            value: battery.name
        }
    });

    const handleOnFinish = async (values) => {
        setLoading(true);
        try {
            const { data } = await createIntervention(battery.id, values);

            notification.success({
                message: data.message,
                description: `Intervention added on location:${values.location}, battery: ${battery.name}`,
            });
            updateSoldToTable(battery);
            handleClose();
            setBattery(null);
        } catch {
            message.error('Something went wrong!');
        } finally {
            setLoading(false);
        }
    }

    return <Modal
        title={'Add Intervention'}
        visible={visible}
        onCancel={handleClose}
        footer={false}
        closable={false}
    >
        <Form onFinish={handleOnFinish}>
            <Form.Item
                name='Battery'
                rules={[
                    {
                        required: true,
                        message: 'Battery is required!'
                    }
                ]}
            >
                <AutoComplete
                    size='large'
                    onSelect={value => setBattery(batteries.find(battery => battery.name === value))}
                    placeholder='Search for available batteries'
                    options={batteryNames}
                />
            </Form.Item>

            <Form.Item
                name="location"
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
                        max: 64,
                        message: "Maximum 16 charachters!",
                    },
                ]}
            >
                <Input size="large" placeholder="Location" />
            </Form.Item>

            <Typography.Text>
                Price is calculated automatically by adding 5000 to the current battery price
            </Typography.Text>

            <InputNumber size="large" min={0} value={battery?.price + 5000} placeholder="Price" />

            <Form.Item style={{ marginTop: '1em' }}>
                <Button type="primary" htmlType="submit" loading={loading}>
                    Add
                </Button>

                <Button
                    loading={loading}
                    type="danger"
                    onClick={() => handleClose()}
                    style={{ marginLeft: "1em" }}
                >
                    Close
                </Button>
            </Form.Item>
        </Form>
    </Modal>
}
