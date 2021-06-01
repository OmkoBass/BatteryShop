import React, { useState } from 'react';

import { createStorage } from "../utils";

import {Button, Form, Input, message, Modal, notification} from "antd";

export default function StorageModal({ visible, handleClose, addStorageToTable }) {
    const [loading, setLoading] = useState(false);

    const handleOnFinish = (values) => {
        (async () => {
            setLoading(true);
            try {
                const { data } = await createStorage(values);

                notification.success({
                    message: "Storage added!",
                    description: `Storage has been added!`,
                });

                addStorageToTable(data);
                handleClose();
            } catch {
                message.error("Something went wrong!");
            } finally {
                setLoading(false);
            }
        })();
    }

    return <Modal
        title="Add Storage"
        visible={visible}
        onCancel={handleClose}
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

            <Form.Item>
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
