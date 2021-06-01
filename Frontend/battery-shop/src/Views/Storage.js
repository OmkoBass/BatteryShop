import React, {useState, useEffect} from "react";

import {useParams, useHistory} from "react-router-dom";

import {Table, message, Button, Input, PageHeader} from "antd";

import AddBatteryModal from "../Components/AddBatteryModal";

import {getStorage, batteryColumns} from "../utils";
import BatteryInfoModal from "../Components/BatteryInfoModal";

export default function Storage() {
    const [batteries, setBatteries] = useState([]);
    const [searchedBatteries, setSearchedBatteries] = useState([]);
    const [batteryModal, setBatteryModal] = useState(false);
    const [selectedBattery, setSelectedBattery] = useState(null);
    const [loading, setLoading] = useState(true);

    const [addEmployeeVisible, setAddEmployeeVisible] = useState(false);
    const history = useHistory();

    const {id} = useParams();

    useEffect(() => {
        (async () => {
            try {
                const {data} = await getStorage(id);

                setBatteries(data.batteries);
            } catch {
                message.error("Something went wrong!");
            } finally {
                setLoading(false);
            }
        })();
    }, [id]);

    return (
        <div>
            <PageHeader
                onBack={() => history.goBack()}
                title={`Storage ${id}`}
                subTitle="Only articles of this storage will be displayed"
                style={{marginLeft: "-2em", marginTop: "-2em"}}
            />
            <Button type="primary" onClick={() => setAddEmployeeVisible(true)}>
                Add Battery
            </Button>

            <Input.Search
                style={{marginTop: "1em"}}
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
                style={{marginTop: "1em"}}
                title={() => "Batteries"}
                rowKey={"id"}
                loading={loading}
                onRow={(record) => {
                    return {
                        onClick: () => {
                            setBatteryModal(true);
                            setSelectedBattery(record);
                        },
                    };
                }}
                dataSource={
                    searchedBatteries.length === 0 ? batteries : searchedBatteries
                }
                columns={batteryColumns}
            />

            <AddBatteryModal
                visible={addEmployeeVisible}
                onClose={() => setAddEmployeeVisible(false)}
                storageId={id}
                addBatteryToTable={(battery) => setBatteries([...batteries, battery])}
            />

            <BatteryInfoModal
                storage={true}
                removeBatteryFromTable={() => {
                    setBatteries(batteries.filter(battery => battery.id !== selectedBattery.id));
                    setSelectedBattery(null);
                }}
                visible={batteryModal}
                handleClose={() => setBatteryModal(false)}
                battery={selectedBattery}
            />
        </div>
    );
}
