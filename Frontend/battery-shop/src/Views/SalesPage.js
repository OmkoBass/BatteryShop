import React, { useState, useEffect } from 'react';

import { getBatteriesByBatteryShop, batteryColumns } from "../utils";

import {Table, message, Input, Button} from "antd";

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

        <Button type="primary" style={{ marginLeft: '1em' }}>
            Intervention
        </Button>

        <Button type="primary" style={{ marginLeft: '1em' }}>
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
            columns={batteryColumns}
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
