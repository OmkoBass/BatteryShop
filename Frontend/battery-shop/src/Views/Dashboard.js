import React, { useEffect, useContext } from "react";

import { Route, Switch, useHistory } from "react-router-dom";

import { getLoggedInEmployee, getBatteryShop } from "../utils";

import { Layout, message } from "antd";

import { AuthContext } from "../Auth";
import HEADER from "../Components/Header";
import AdminPage from "./AdminPage";
import axios from "axios";

const { Content, Footer } = Layout;

export default function Dashboard() {
  const { currentUser, setBatteryShop, setEmployee } = useContext(AuthContext);

  const history = useHistory();

  axios.defaults.headers.get["Authorization"] = currentUser;
  axios.defaults.headers.put["Authorization"] = currentUser;
  axios.defaults.headers.post["Authorization"] = currentUser;

  useEffect(() => {
    if (!currentUser) {
      history.push("/login");
    } else {
      (async () => {
        try {
          const { data: employee } = await getLoggedInEmployee();
          const { data: batteryShop } = await getBatteryShop();

          setEmployee(employee);
          setBatteryShop(batteryShop);

          if (employee.job === 4) {
            history.push("/admin");
          } else if (employee.job === 3) {
            console.log("INTERVETION");
          } else if (employee.job === 2) {
            console.log("SUPPLY");
          } else if (employee.job === 1) {
            console.log("SALES");
          } else {
            console.log("SERVICE");
          }
        } catch {
          message.error("Something went wrong!");
        } finally {
        }
      })();
    }
  }, [currentUser, history, setBatteryShop, setEmployee]);

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <HEADER />
      <Content style={{ padding: "2em" }}>
        <Switch>
          <Route path={`/admin`} component={AdminPage} />
        </Switch>
      </Content>
      <Footer style={{ textAlign: "center" }}>@OmkoBass</Footer>
    </Layout>
  );
}