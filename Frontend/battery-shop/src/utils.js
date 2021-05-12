import axios from "axios";

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
};
