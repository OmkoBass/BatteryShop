import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import { AuthProvider } from "./Auth";

import "antd/dist/antd.css";
import "./Styles/style.css";

import Login from "./Views/Login";
import Dashboard from "./Views/Dashboard";

function App() {
  return (
    <AuthProvider>
      <Router>
        <Switch>
          <Route path="/login" component={Login} />

          <Route path="/" component={Dashboard} />
        </Switch>
      </Router>
    </AuthProvider>
  );
}

export default App;
