import React, { useState, useEffect } from 'react';
import {Input, message, Table} from "antd";
import {getCustomersForBatteryShop} from "../utils";

export default function CustomerPage() {
    const [customers, setCustomers] = useState([]);
    const [searchedCustomers, setSearchedCustomers] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        (async () => {
            try {
                const { data } = await getCustomersForBatteryShop();

                setCustomers(data);
            } catch {
                message.error('Something went wrong!');
            } finally {
                setLoading(false);
            }
        })();
    }, []);

    const customerColumns = [
        {
            title: "Id",
            dataIndex: "id",
        },
        {
            title: "Name",
            dataIndex: "name",
        },
        {
            title: "Last Name",
            dataIndex: "lastName",
        },
        {
            title: "Address",
            dataIndex: "address",
        }
    ];

    return <div>
        <Input.Search
            style={{ marginTop: "1em" }}
            placeholder={"Enter name of the customer"}
            allowClear
            onSearch={(value) => {
                if (value) {
                    console.log(value);
                    setSearchedCustomers(
                        customers.filter((customer) => customer.name.includes(value))
                    );
                } else {
                    setSearchedCustomers([]);
                }
            }}
        />

        <Table
            style={{ marginTop: "1em" }}
            title={() => "Customers"}
            rowKey={"id"}
            loading={loading}
            dataSource={searchedCustomers.length === 0 ? customers : searchedCustomers}
            columns={customerColumns}
        />
    </div>
}
