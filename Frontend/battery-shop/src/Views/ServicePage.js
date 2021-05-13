import React, { useState, useEffect } from 'react';

import {Table, Input, message} from "antd";

import { getSoldBatteriesByBatteryShop, batteryColumns } from "../utils";
import BatteryInfoModal from "../Components/BatteryInfoModal";

export default function ServicePage() {
    const [batteries, setBatteries] = useState([]);
    const [searchedBatteries, setSearchedBatteries] = useState([]);
    const [loading, setLoading] = useState(true);
    const [batteryModal, setBatteryModal] = useState(false);
    const [selectedBattery, setSelectedBattery] = useState(null);

    useEffect(() => {
        (async () => {
            try {
                const { data } = await getSoldBatteriesByBatteryShop();

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
            columns={batteryColumns}
            onRow={(record) => {
                return {
                    onClick: () => {
                        setBatteryModal(true);
                        setSelectedBattery(record);
                    },
                };
            }}
        />

        <BatteryInfoModal
            removeBatteryFromTable={() => {
                setBatteries(batteries.filter(battery => battery.id !== selectedBattery.id));
                setSelectedBattery(null);
            }}
            visible={batteryModal}
            handleClose={() => setBatteryModal(false)}
            battery={selectedBattery}
        />
    </div>
}
