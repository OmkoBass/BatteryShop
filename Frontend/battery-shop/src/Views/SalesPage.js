import React, { useState, useEffect } from 'react';

import { getBatteriesByBatteryShop } from "../utils";

import {Table, message, Typography, Input, Button} from "antd";

import {DollarTwoTone, ThunderboltTwoTone} from "@ant-design/icons";
import SellBatteryModal from "../Components/SellBatteryModal";

export default function SalesPage() {
    const [batteries, setBatteries] = useState([]);
    const [searchedBatteries, setSearchedBatteries] = useState([]);
    const [selectedBattery, setSelectedBattery] = useState(null);
    const [sellBatteryModal, setSellBatteryModal] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        (async () => {
            try {
                const { data } = await getBatteriesByBatteryShop();

                setBatteries(data);
            } catch {
                message.error('Something went wrong!');
            } finally {
                setLoading(false);
            }
        })();
    }, []);

    const columns = [
        {
            title: "Id",
            dataIndex: "id",
        },
        {
            title: "Name",
            dataIndex: "name",
        },
        {
            title: () => (
                <Typography.Text>
                    Life <ThunderboltTwoTone twoToneColor="#ffec3d" />
                </Typography.Text>
            ),
            dataIndex: "life",
            render: (data) => <Typography.Text>{data}</Typography.Text>,
        },
        {
            title: () => (
                <Typography.Text>
                    Price <DollarTwoTone twoToneColor="#52c41a" />
                </Typography.Text>
            ),
            dataIndex: "price",
            sorter: (a, b) => a.price - b.price,
        },
        {
            title: 'Sold',
            dataIndex: 'sold',
            render: (text) => (<Typography.Text> {text ? 'SOLD' : 'Available'} </Typography.Text>),
            sorter: (a, b) => a.sold - b.sold
        }
    ];

    const rowSelection = {
        onSelect: (record) => {
            if(!record.sold) {
                setSelectedBattery(record);
            }
        }
    };

    return <div>
        <Button type="primary" disabled={!selectedBattery} onClick={() => setSellBatteryModal(true)}>
            Sell
        </Button>

        <Input.Search
            style={{ marginTop: "1em" }}
            placeholder={"Enter name of the battery"}
            allowClear
            onSearch={(value) => {
                if (value) {
                    setSearchedBatteries(
                        batteries.filter((battery) => battery.name.includes(value))
                    );
                } else {
                    setSearchedBatteries([]);
                }
            }}
        />

        <Table
            rowSelection={{
                type: 'radio',
                ...rowSelection
            }}
            style={{ marginTop: "1em" }}
            title={() => "Batteries"}
            rowKey={"id"}
            loading={loading}
            dataSource={searchedBatteries.length === 0 ? batteries : searchedBatteries}
            columns={columns}
        />

        <SellBatteryModal
            visible={sellBatteryModal}
            handleClose={() => setSellBatteryModal(false)}
            battery={selectedBattery}
            updateSoldToTable={received => {
                setBatteries(batteries.map(battery => {
                    if(battery.id === received.id) {
                        return received;
                    }
                    return battery;
                }))
            }}
        />
    </div>
}
