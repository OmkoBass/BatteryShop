import React, { useContext } from "react";

import { useHistory } from "react-router-dom";

import { Layout, Avatar, Menu, Dropdown } from "antd";

import { AuthContext } from "../Auth";

import { UserOutlined, LogoutOutlined } from "@ant-design/icons";

import Logo from "../Assets/eBattery.PNG";

const { Header } = Layout;

export default function HEADER() {
  const { setCurrentUser, employee, batteryShop } = useContext(AuthContext);

  const history = useHistory();

  const HeaderMenu = () => {
    return (
      <Menu
        style={{ width: "250px" }}
        onClick={() => {
          localStorage.removeItem("BatteryShopToken");
          setCurrentUser(null);
          history.push("/login");
        }}
      >
        <Menu.Item icon={<LogoutOutlined />}>Logout</Menu.Item>
      </Menu>
    );
  };

  return (
    <Header className="background-blue flex-row">
      <img src={Logo} alt={"Logo"} style={{ height: '100%' }}/>
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
