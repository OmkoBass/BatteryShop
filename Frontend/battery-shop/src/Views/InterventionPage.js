import React, { useState, useEffect } from 'react';

import {Table, Button, message, Typography, Input} from "antd";

import { getInterventionsForBatteryShop } from "../utils";

import {DollarTwoTone} from "@ant-design/icons";
import ResolveInterventionModal from "../Components/ResolveInterventionModal";

export default function InterventionPage() {
    const [interventions, setInterventions] = useState([]);
    const [searchedInterventions, setSearchedInterventions] = useState([]);
    const [intervention, setIntervention] = useState(null);
    const [interventionModal, setInterventionModal] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        (async () => {
            try {
                const { data } = await getInterventionsForBatteryShop();

                setInterventions(data);
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
            title: "Location",
            dataIndex: "location",
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
            title: "Battery Id",
            dataIndex: "batteryId",
        },
        {
            title: "Description",
            dataIndex: "description",
        },
        {
            title: () => (
                <Typography.Text>
                    Action
                </Typography.Text>
            ),
            key: 'action',
            render: (_, record) => <Button type="primary" onClick={() => {
                setInterventionModal(true);
                setIntervention(record);
            }}>
                {
                    record.resolved
                        ?
                        'Info'
                        :
                        'Resolve Intervention'
                }
            </Button>
        }
    ]

    return <div>
        <Input.Search
            style={{ marginTop: "1em" }}
            placeholder={"Enter name of the battery"}
            allowClear
            onSearch={(value) => {
                if (value) {
                    setSearchedInterventions(
                        interventions.filter((intervention) => intervention.location.includes(value))
                    );
                } else {
                    setSearchedInterventions([]);
                }
            }}
        />

        <Table
            style={{ marginTop: '1em' }}
            title={() => 'Interventions'}
            dataSource={searchedInterventions.length === 0 ? interventions : searchedInterventions}
            rowKey={"id"}
            columns={columns}
            loading={loading}
        />

        <ResolveInterventionModal
            visible={interventionModal}
            handleClose={() => setInterventionModal(false)}
            updateToInterventionTable={intervention => {
                setInterventions(interventions.map(interv => {
                    if (interv.id === intervention.id) {
                        return intervention;
                    }
                    return interv;
                }));
            }}
            intervention={intervention}
        />
    </div>
}