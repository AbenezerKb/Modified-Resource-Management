import React, { Component, useEffect } from "react";
import axios from "./api/api";

import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import ContextProviders from "./contexts/ContextProviders";
import Sidebar from "./components/layouts/Sidebar";
import MaterialTransfer from "./components/forms/transfer/MaterialTransfer";
import MaterialPurchase from "./components/forms/purchase/MaterialPurchase";
import MaterialItem from "./components/forms/item/MaterialItem";
import ListMySiteReceives from "./components/forms/receive/ListMySiteReceives";
import MaterialBuy from "./components/forms/buy/MaterialBuy";
import EquipmentTransfer from "./components/forms/transfer/EquipmentTransfer";
import ListTransfers from "./components/forms/transfer/ListTransfers";
import ListPurchases from "./components/forms/purchase/ListPurchases";
import ListBuys from "./components/forms/buy/ListBuys";
import SingleTransfer from "./components/forms/transfer/SingleTransfer";
import SinglePurchase from "./components/forms/purchase/SinglePurchase";
import SingleBuy from "./components/forms/buy/SingleBuy";
import SingleReceive from "./components/forms/receive/SingleReceive";
import ListReceives from "./components/forms/receive/ListReceives";

import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import ListIssues from "./components/forms/issue/ListIssues";
import SingleIssue from "./components/forms/issue/SingleIssue";
import NewIssue from "./components/forms/issue/NewIssue";
import ListBorrows from "./components/forms/borrow/ListBorrows";
import SingleBorrow from "./components/forms/borrow/SingleBorrow";
import NewBorrow from "./components/forms/borrow/NewBorrow";
import ListMaintenance from "./components/forms/maintenance/ListMaintenance";
import SingleMaintenance from "./components/forms/maintenance/SingleMaintenance";
import NewMaintenance from "./components/forms/maintenance/NewMaintenance";
import ListReturns from "./components/forms/return/ListReturns";
import NewReturn from "./components/forms/return/NewReturn";
import SingleReturn from "./components/forms/return/SingleReturn";
import SignUp from "./components/forms/authorization/RegistrationForm";
import Login from "./components/forms/authorization/Login";
import TransferReport from "./components/forms/report/TransferReport";
import { useAuth, useAuthUpdate } from "./contexts/AuthContext";
import EquipmentItem from "./components/forms/item/EquipmentItem";
import EquipmentModel from "./components/forms/item/EquipmentModel";
import EquipmentCategory from "./components/forms/item/EquipmentCategory";
import ManageUser from "./components/forms/user/ManageUser";
import NewRole from "./components/forms/user/NewRole";
import Dashboard from "./components/forms/Dashboard";
import Report from "./components/forms/report/Report";
import ListEmployee from "./components/forms/employee/ListEmployee";
import SingleEmployee from "./components/forms/employee/SingleEmployee";
import NewSite from "./components/forms/site/NewSite";
import NewRent from "./components/forms/rent/NewRent";
import AdminNotification from "./components/forms/AdminNotification";
import MinimumStock from "./components/forms/item/MinimumStock";
import ComanyNamePrefix from "./components/forms/misc/ComanyNamePrefix";

const App = () => {
    const contextData = useAuth();
    const updateAuthContext = useAuthUpdate();

    useEffect(() => {
        updateAuthContext({
            ...contextData,
            token: localStorage.getItem("token"),
            data: JSON.parse(localStorage.getItem("data")),
        });
    }, []);
    useEffect(() => {
        if (contextData.token) {
            axios.defaults.headers.common["Authorization"] = "bearer " + contextData.token;
        }
    });
    return (
        <Router>
            {contextData?.token ? (
                <div
                    className="row m-0 flex-nowrap"
                    style={{
                        minHeight: "100vh",
                        maxWidth: "100vw",
                        minWidth: "900px",
                    }}
                >
                    <div className="col-auto p-0">
                        <Sidebar />
                    </div>
                    <div className="col p-0">
                        <Routes>
                            <Route path="/login" element={<Navigate to={"/"} />} />
                            <Route path="/register" element={<Navigate to={"/"} />} />

                            <Route path="/" element={<Dashboard />} />

                            <Route path="employee" element={<ListEmployee />} />
                            <Route path="employee/:id" element={<SingleEmployee />} />

                            <Route path="user/manage" element={<ManageUser />} />

                            <Route path="role/new" element={<NewRole />} />

                            <Route path="site/new" element={<NewSite />} />

                            <Route path="report" element={<Report />} />

                            <Route path="item/minimum-stock" element={<MinimumStock />} />
                            <Route path="item/material/new" element={<MaterialItem />} />
                            <Route path="item/equipment/new" element={<EquipmentItem />} />
                            <Route path="item/equipment/model/new" element={<EquipmentModel />} />
                            <Route
                                path="item/equipment/category/new"
                                element={<EquipmentCategory />}
                            />

                            <Route path="issue" element={<ListIssues />} />
                            <Route path="issue/:id" element={<SingleIssue />} />
                            <Route path="issue/new" element={<NewIssue />} />

                            <Route path="borrow" element={<ListBorrows />} />
                            <Route path="borrow/:id" element={<SingleBorrow />} />
                            <Route path="borrow/new" element={<NewBorrow />} />

                            <Route path="return" element={<ListReturns />} />
                            <Route path="return/:id" element={<SingleReturn />} />
                            <Route path="return/new" element={<NewReturn />} />

                            <Route path="maintenance" element={<ListMaintenance />} />
                            <Route path="maintenance/:id" element={<SingleMaintenance />} />
                            <Route path="maintenance/new" element={<NewMaintenance />} />

                            <Route path="transfer/material" element={<MaterialTransfer />} />
                            <Route path="transfer/equipment" element={<EquipmentTransfer />} />
                            <Route
                                path="transfer/in"
                                element={<ListTransfers title="Transfer In" type="in" />}
                            />
                            <Route
                                path="transfer/out"
                                element={<ListTransfers title="Transfer Out" type="out" />}
                            />
                            <Route path="transfer/:id" element={<SingleTransfer />} />

                            <Route path="buy" element={<ListBuys />} />
                            <Route path="buy/:id" element={<SingleBuy />} />
                            <Route path="buy/material" element={<MaterialBuy />} />

                            <Route path="purchase" element={<ListPurchases />} />
                            <Route path="purchase/:id" element={<SinglePurchase />} />
                            <Route path="purchase/material" element={<MaterialPurchase />} />

                            <Route path="receive" element={<ListReceives />} />
                            <Route path="receive/:id" element={<SingleReceive />} />
                            <Route path="receive/material" element={<ListMySiteReceives />} />

                            <Route path="notification" element={<AdminNotification />} />
                            <Route path="misc/company" element={<ComanyNamePrefix />} />

                            <Route path="rent/new" element={<NewRent />} />
                        </Routes>
                    </div>
                </div>
            ) : (
                <div
                    className="row m-0"
                    style={{
                        minHeight: "100vh",
                        maxWidth: "100vw",
                        minWidth: "900px",
                    }}
                >
                    <div>
                        <Routes>
                            <Route path="/login" element={<Login />} />
                            <Route path="/" element={<Navigate to={"/login"} />} />

                            <Route path="/registration" element={<SignUp />} />
                        </Routes>
                    </div>
                </div>
            )}
        </Router>
    );
};

export default App;
