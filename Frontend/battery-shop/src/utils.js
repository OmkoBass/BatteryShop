import axios from "axios";

const BACKEND = `http://localhost:57661`;

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

const getAllBatteries = async (value) => {
  return await axios.get(`${BACKEND}/api/Battery`);
};

const getBattery = async (id) => {
  return await axios.get(`${BACKEND}/api/Battery/:id?Id=${id}`);
};

export {
  sendLoginInfo,
  getAllBatteries,
  getBattery,
  getBatteryShop,
  getLoggedInEmployee,
  getBatteryShopEmployees,
};
