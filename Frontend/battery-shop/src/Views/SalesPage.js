import React, { useState, useEffect } from 'react';

import { useHistory } from "react-router-dom";

import { getBatteriesByBatteryShop, batteryColumns } from "../utils";

import {Table, message, Input, Button, Typography} from "antd";

import SellBatteryModal from "../Components/SellBatteryModal";
import AddInterventionModal from "../Components/AddInterventionModal";

export default function SalesPage() {
    const [batteries, setBatteries] = useState([]);
    const [searchedBatteries, setSearchedBatteries] = useState([]);
    const [selectedBattery, setSelectedBattery] = useState(null);
    const [sellBatteryModal, setSellBatteryModal] = useState(false);
    const [interventionModal, setInterventionModal] = useState(false);
    const [loading, setLoading] = useState(true);

    const history = useHistory();

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

    const rowSelection = {
        onSelect: (record) => {
            if(!record.sold) {
                setSelectedBattery(record);
            }
        }
    };

    return <div>
        <Typography.Title level={4}>
            You can sell batteries to our customers, create interventions and check warranties here.
        </Typography.Title>

        <Button type="primary" disabled={!selectedBattery} onClick={() => setSellBatteryModal(true)}>
            Sell
        </Button>

        <Button type="primary" style={{ marginLeft: '1em' }} onClick={() => setInterventionModal(true)}>
            Intervention
        </Button>

        <Button type="primary" style={{ marginLeft: '1em' }} onClick={() => history.push('sales/warranties')}>
            Warranties
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
            columns={batteryColumns
            }
        />

        <SellBatteryModal
            visible={sellBatteryModal}
            handleClose={() => setSellBatteryModal(false)}
            battery={selectedBattery}
            updateSoldToTable={received => {
                setBatteries(batteries.filter(battery => battery.id !== received.id));
                setSelectedBattery(null);
            }}
        />

        <AddInterventionModal
            visible={interventionModal}
            handleClose={() => setInterventionModal(false)}
            batteries={batteries}
            updateSoldToTable={(received) => {
                setBatteries(batteries.filter(battery => battery.id !== received.id));
                setSelectedBattery(null);
            }}
        />
    </div>
}
