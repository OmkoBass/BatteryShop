import React, { useState, useContext } from "react";

import { Redirect, useHistory } from "react-router";

import { sendLoginInfo } from "../utils";

import { AuthContext } from "../Auth";

import { Divider, Button, Form, Input, message } from "antd";
import { LockOutlined, UserOutlined } from "@ant-design/icons";

import "../Styles/login.css";

export default function Login() {
  const { currentUser, setCurrentUser } = useContext(AuthContext);

  const [loading, setLoading] = useState(false);

  const history = useHistory();

  if (currentUser) return <Redirect to="/" />;

  const handleOnFinish = async (value) => {
    setLoading(true);
    try {
      const { data } = await sendLoginInfo(value);

      setCurrentUser(data.token);
      history.push("/");
    } catch {
      message.error("Check your username or password!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login">
      <div className="login-form">
        <h1> Battery Shop </h1>

        <Divider />

        <Form onFinish={handleOnFinish}>
          <Form.Item
            name="username"
            rules={[
              {
                required: true,
                message: "Enter your username!",
              },
            ]}
          >
            <Input
              prefix={<UserOutlined />}
              size="large"
              className="login-form-input"
              placeholder="Username"
            />
          </Form.Item>

          <Form.Item
            name="password"
            rules={[
              {
                required: true,
                message: "Enter your password!",
              },
            ]}
          >
            <Input.Password
              prefix={<LockOutlined />}
              size="large"
              className="login-form-input"
              placeholder="Password"
            />
          </Form.Item>

          <Form.Item>
            <Button
              loading={loading}
              type="primary"
              block={true}
              htmlType="submit"
            >
              Log In
            </Button>
          </Form.Item>
        </Form>
      </div>
    </div>
  );
}
