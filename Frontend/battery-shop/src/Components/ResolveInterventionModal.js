import React, { useState } from 'react';

import { resolveIntervention } from "../utils";

import {Modal, Form, Button, Input, Descriptions, Divider, message, notification } from "antd";

export default function ResolveInterventionModal({ visible, handleClose, updateToInterventionTable, intervention}) {
    const [loading, setLoading] = useState(false);

    const handleOnFinish = async (values) => {
        setLoading(true);
        try {
            const { data } = await resolveIntervention(intervention.id, intervention.batteryId, values);

            notification.success({
                message: 'Intervention resolved',
                description: `Intervention resolved on location:${values.location}!`,
            });

            updateToInterventionTable(data);
            handleClose();
        } catch {
            message.error('Something went wrong!');
        } finally {
            setLoading(false);
        }
    }

    return <Modal
        header={"Resolve Intervention"}
        visible={visible}
        onCancel={handleClose}
        footer={false}
    >
        <Descriptions title={"Intervention Info"} bordered layout="vertical">
            <Descriptions.Item label={"Location"}>
                {intervention?.location}
            </Descriptions.Item>

            <Descriptions.Item label={"Location"}>
                {intervention?.price}
            </Descriptions.Item>

            {
                intervention?.resolved
                ?
                    <Descriptions.Item label={"Description"} span={2}>
                        {intervention?.description}
                    </Descriptions.Item>
                    :
                    null
            }
        </Descriptions>

        {
            !intervention?.resolved
            ?
                <div>
                    <Divider> Complete Intervention </Divider>
                    <Form onFinish={handleOnFinish}>
                        <Form.Item
                            name="description"
                            rules={[
                                {
                                    required: true,
                                    message: 'Description is required!'
                                },
                                {
                                    min: 3,
                                    message: 'Atleast 3 characters!'
                                },
                                {
                                    max: 256,
                                    message: 'Maximum 256 characters!'
                                }
                            ]}
                        >
                            <Input.TextArea
                                placeholder="Description"
                                autoSize={{ minRows: 3, maxRows: 5 }}
                            />
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

                            <Button style={{ marginLeft: "1em" }} loading={loading} type={"danger"} onClick={() => handleClose()}>
                                Cancel
                            </Button>
                        </Form.Item>
                    </Form>
                </div>
                :
                null
        }
    </Modal>
}
