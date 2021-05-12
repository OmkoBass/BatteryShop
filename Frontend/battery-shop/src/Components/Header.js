import React, { useContext } from "react";

import { Layout, Avatar, Menu, Dropdown } from "antd";

import { AuthContext } from "../Auth";

import { UserOutlined, LogoutOutlined } from "@ant-design/icons";

const { Header } = Layout;

const HeaderMenu = () => {
  return (
    <Menu style={{ width: "250px" }}>
      <Menu.Item icon={<LogoutOutlined />}>Logout</Menu.Item>
    </Menu>
  );
};

export default function HEADER() {
  const { employee, batteryShop } = useContext(AuthContext);

  return (
    <Header className="background-blue flex-row">
      <h1 style={{ color: "white" }}>Battery Shop</h1>
      <h1 style={{ color: "white" }}> {batteryShop?.name} </h1>
      <div className="flex-row">
        <h1 style={{ color: "white" }}> {employee?.name} </h1>
        <Dropdown overlay={HeaderMenu} trigger="click">
          <Avatar
            style={{ marginLeft: "1em", cursor: "pointer" }}
            size={48}
            icon={<UserOutlined />}
          />
        </Dropdown>
      </div>
    </Header>
  );
}
