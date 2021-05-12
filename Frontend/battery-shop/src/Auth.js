import React, { useEffect, useState } from "react";

export const AuthContext = React.createContext(undefined, undefined);

export const AuthProvider = ({ children }) => {
  const [currentUser, setCurrentUser] = useState(
    JSON.parse(localStorage.getItem("BatteryShopToken"))
  );

  const [batteryShop, setBatteryShop] = useState(null);
  const [employee, setEmployee] = useState(null);

  useEffect(() => {
    if (currentUser !== null) {
      localStorage.setItem("BatteryShopToken", JSON.stringify(currentUser));
      setCurrentUser(currentUser);
    } else {
      setCurrentUser(currentUser);
    }
  }, [currentUser]);

  return (
    <AuthContext.Provider
      value={{
        currentUser,
        setCurrentUser,
        batteryShop,
        setBatteryShop,
        employee,
        setEmployee,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
