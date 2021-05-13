import React, { useState, useEffect } from 'react'

import {Table, message, Input, Typography, Button, notification} from "antd";

import { getReplacementBatteries, replaceBattery } from "../utils";
import {DollarTwoTone, ThunderboltTwoTone} from "@ant-design/icons";

export default function WarrantiesPage() {
    const [batteries, setBatteries] = useState([]);
    const [searchedBatteries, setSearchedBatteries] = useState([]);
    const [loading, setLoading] = useState(true);

    const handleReplaceBattery = async (id) => {
        setLoading(true);
        try {
            const { data } = await replaceBattery(id);

            notification.success({
                message: "Replacement successful!",
                description: data.message,
                duration: 10
            });

            setBatteries(batteries.filter(battery => battery.id !== id));
        } catch {
            message.error('Something went wrong!');
        } finally {
            setLoading(false);
        }
    }

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
        },
        {
            title: () => (
                <Typography.Text>
                    Action
                </Typography.Text>
            ),
            key: 'action',
            render: (_, record) => {
                return <Button type="primary" onClick={() => handleReplaceBattery(record.id)}>
                    Give new battery
                </Button>
            }
        }
    ];

    useEffect(() => {
        (async () => {
            try {
                const { data } = await getReplacementBatteries();

                setBatteries(data);
            } catch {
                message.error('Something went wrong!');
            } finally {
                setLoading(false);
            }
        })();
    }, []);

    return <div>
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
            style={{ marginTop: "1em" }}
            title={() => "Batteries"}
            rowKey={"id"}
            loading={loading}
            dataSource={searchedBatteries.length === 0 ? batteries : searchedBatteries}
            columns={columns}
        />
    </div>
}
