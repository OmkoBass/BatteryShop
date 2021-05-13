import React from "react";

import axios from "axios";

import { Typography } from "antd";

import { DollarTwoTone, ThunderboltTwoTone } from "@ant-design/icons";

const BACKEND = `http://localhost:57661`;

const handleDisplayProperJob = (jobId) => {
  if (jobId === 4) {
    return "Admin";
  } else if (jobId === 3) {
    return "Intervetion";
  } else if (jobId === 2) {
    return "Supply";
  } else if (jobId === 1) {
    return "Sales";
  } else {
    return "Service";
  }
};

const sendLoginInfo = async (value) => {
  return await axios.post(`${BACKEND}/authenticate`, {
    username: value.username.toLowerCase(),
    password: value.password,
  });
};

const getLoggedInEmployee = async () => {
  return await axios.get(`${BACKEND}/api/Employee/loggedIn`);
};

const getBatteryShopEmployees = async () => {
  return await axios.get(
    `${BACKEND}/api/Employee/loggedIn/batteryShop/employees`
  );
};

const getBatteryShop = async () => {
  return await axios.get(`${BACKEND}/api/Employee/loggedIn/batteryShop`);
};

const getAllBatteries = async () => {
  return await axios.get(`${BACKEND}/api/Battery`);
};

const getBattery = async (id) => {
  return await axios.get(`${BACKEND}/api/Battery/:id?Id=${id}`);
};

const deleteEmployee = async (id) => {
  id = parseInt(id);
  return await axios.delete(`${BACKEND}/api/Employee/:id?Id=${id}`);
};

const addEmployee = async (values) => {
  values.job = parseInt(values.job);
  return await axios.post(`${BACKEND}/api/Employee`, values);
};

const getStorages = async () => {
  return await axios.get(`${BACKEND}/api/Storage/loggedIn`);
};

const getStorage = async (id) => {
  return await axios.get(`${BACKEND}/api/Storage/:id?Id=${id}`);
};

const addBattery = async (battery) => {
  return await axios.post(`${BACKEND}/api/Battery`, battery);
};

const getBatteriesByBatteryShop = async () => {
  return await axios.get(`${BACKEND}/api/Battery/batteryShop`);
}

const getSoldBatteriesByBatteryShop = async () => {
  return await axios.get(`${BACKEND}/api/Battery/sold/`);
}

const sellBattery = async (customer, id) => {
  return await axios.post(`${BACKEND}/api/Battery/sell/:batteryId?BatteryId=${id}`, customer);
}

const getCustomer = async (id) => {
  return await axios.get(`${BACKEND}/api/Customer/:id?Id=${id}`);
}

const handleCheckBattery = async (id) => {
  return await axios.get(`${BACKEND}/api/Battery/check/:id?Id=${id}`);
}

const getReplacementBatteries = async () => {
  return await axios.get(`${BACKEND}/api/Battery/replacement`);
}

const replaceBattery = async (id) => {
  return await axios.get(`${BACKEND}/api/Battery/replace/:id?Id=${id}`);
}

const batteryColumns = [
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

export {
  sendLoginInfo,
  getAllBatteries,
  getBattery,
  getBatteryShop,
  getLoggedInEmployee,
  getBatteryShopEmployees,
  deleteEmployee,
  handleDisplayProperJob,
  addEmployee,
  getStorages,
  getStorage,
  addBattery,
  getBatteriesByBatteryShop,
  getSoldBatteriesByBatteryShop,
  sellBattery,
  getCustomer,
  handleCheckBattery,
  getReplacementBatteries,
  replaceBattery,
  batteryColumns,
};
