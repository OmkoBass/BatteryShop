import React, { useState, useEffect } from 'react';
import {Descriptions, Modal, message, Button, notification } from "antd";

import { getCustomer, handleCheckBattery } from "../utils";

export default function BatteryInfoModal({ visible, handleClose, battery, removeBatteryFromTable }) {
    const [customer, setCustomer] = useState(null);
    const [loading, setLoading] = useState(true);

    const handleCheckWarranty = async () => {
        setLoading(true);
        try {
            const { data } = await handleCheckBattery(battery.id);

            notification.success({
                message: "Battery checked!",
                description: data.message,
                duration: 25
            });

            if(data.message === 'Choose a new battery!') {
                removeBatteryFromTable();
            }

            handleClose();
        } catch {
            message.error('Something went wrong!');
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        if(visible) {
            (async () => {
                try {
                    const { data } = await getCustomer(battery.customerId);

                    setCustomer(data);
                } catch {
                    message.error('Something went wrong!');
                } finally {
                    setLoading(false);
                }
            })();
        }
    }, [visible]);

    return <Modal
        load
        visible={visible}
        onCancel={handleClose}
        title={battery?.name}
        footer={false}
        closable={false}
    >
        <Descriptions title="Battery info" bordered layout="vertical">
            <Descriptions.Item label="Name">
                {" "}
                {battery?.name}{" "}
            </Descriptions.Item>
            <Descriptions.Item label="Price"> {battery?.price} </Descriptions.Item>
            <Descriptions.Item label="Life">
                {battery?.life}
            </Descriptions.Item>
            <Descriptions.Item label="Customer">
                {customer?.name} {customer?.lastName}
            </Descriptions.Item>
            <Descriptions.Item label="Customer Address">
                {customer?.name} {customer?.address}
            </Descriptions.Item>
        </Descriptions>

        <Button style={{ marginTop: "1em" }} type="primary" loading={loading}
            onClick={() => handleCheckWarranty()}
        >
            Check Warranty
        </Button>

        <Button style={{ marginLeft: "1em" }} type="danger" onClick={() => handleClose()} loading={loading}>
            Close
        </Button>
    </Modal>
}
