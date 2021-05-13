import React, { useEffect, useContext } from "react";

import { Route, Switch, useHistory, useLocation } from "react-router-dom";

import { getLoggedInEmployee, getBatteryShop } from "../utils";

import { Layout, message } from "antd";

import { AuthContext } from "../Auth";
import HEADER from "../Components/Header";
import AdminPage from "./AdminPage";
import axios from "axios";
import StoragePage from "./StoragePage";
import Storage from "./Storage";

const { Content, Footer } = Layout;

export default function Dashboard() {
  const { currentUser, setBatteryShop, setEmployee } = useContext(AuthContext);

  const history = useHistory();
  const { pathname } = useLocation();

  axios.defaults.headers.get["Authorization"] = currentUser;
  axios.defaults.headers.put["Authorization"] = currentUser;
  axios.defaults.headers.post["Authorization"] = currentUser;
  axios.defaults.headers.delete["Authorization"] = currentUser;

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

          if (pathname === "/") {
            if (employee.job === 4) {
              history.push("/admin");
            } else if (employee.job === 3) {
              console.log("INTERVENTION");
            } else if (employee.job === 2) {
              history.push("/storage");
            } else if (employee.job === 1) {
              console.log("SALES");
            } else {
              console.log("SERVICE");
            }
          }
        } catch {
          message.error("Something went wrong!");
        } finally {
        }
      })();
    }
  }, [currentUser, history, pathname, setBatteryShop, setEmployee]);

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <HEADER />
      <Content style={{ padding: "2em" }}>
        <Switch>
          <Route path={`/storage/:id`} component={Storage} />
          <Route path={`/admin`} component={AdminPage} />
          <Route path={`/storage`} component={StoragePage} />
        </Switch>
      </Content>
      <Footer style={{ textAlign: "center" }}>@OmkoBass</Footer>
    </Layout>
  );
}
