import { useState } from "react";
import {
    ProSidebar,
    Menu,
    MenuItem,
    SubMenu,
    SidebarHeader,
    SidebarFooter,
    SidebarContent,
} from "react-pro-sidebar";
import "react-pro-sidebar/dist/css/styles.css";
import { GoSettings } from "react-icons/go";
import { FaHome, FaList, FaBars, FaUser } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import { useAuth, useAuthUpdate } from "../../contexts/AuthContext";
import { useQueryClient } from "react-query";

function Sidebar() {
    const navigate = useNavigate();
    const updateAuthContext = useAuthUpdate();

    const { data } = useAuth();

    const [collapsed, setCollapsed] = useState(false);

    return (
        <>
            <ProSidebar
                className="text-light sidebar-decrease-padding"
                collapsed={collapsed}
                width="255px"
                style={{ height: "100vh", position: "sticky", top: 0 }}
            >
                <SidebarHeader className="p-1 text-nowrap">
                    <MenuItem
                        className="ms-4 me-3"
                        icon={<FaBars className="fs-5" />}
                        style={{ display: "inline-flex" }}
                        onClick={() => setCollapsed(!collapsed)}
                    />
                    {!collapsed && "System Name"}
                </SidebarHeader>
                <SidebarContent>
                    <Menu>
                        <MenuItem className="" icon={<FaHome className="fs-5" />}>
                            Home <Link to="/" />
                        </MenuItem>

                        <SubMenu title="Setting" icon={<GoSettings className="fs-5" />}>
                            <SubMenu title="Employee">
                                <MenuItem>
                                    Manage Employees <Link to="/employee" />
                                </MenuItem>
                            </SubMenu>
                            <SubMenu title="Role">
                                <MenuItem>
                                    Add a new Role <Link to="/role/new" />
                                </MenuItem>
                            </SubMenu>
                            <SubMenu title="Site">
                                <MenuItem>
                                    Add a new Site <Link to="/site/new" />
                                </MenuItem>
                            </SubMenu>
                            <SubMenu title="Stock">
                                <MenuItem>
                                    Add a new Material <Link to="/item/material/new" />
                                </MenuItem>
                                <SubMenu title="Equipment">
                                    <MenuItem>
                                        Add a new Equipment <Link to="/item/equipment/new" />
                                    </MenuItem>
                                    <MenuItem>
                                        Add a new Model <Link to="/item/equipment/model/new" />
                                    </MenuItem>
                                    <MenuItem>
                                        Add a new Category{" "}
                                        <Link to="/item/equipment/category/new" />
                                    </MenuItem>
                                </SubMenu>

                                <MenuItem>
                                    Minimum Stock <Link to="/item/minimum-stock" />
                                </MenuItem>
                            </SubMenu>
                            <MenuItem>
                                Reports <Link to="/report" />
                            </MenuItem>
                            <MenuItem>
                                Notifications <Link to="/notification" />
                            </MenuItem>
                            <MenuItem>
                                Company Name / Prefix <Link to="/misc/company" />
                            </MenuItem>
                        </SubMenu>
                        <SubMenu
                            title="Inventory"
                            icon={<FaList className="fs-5" />}
                            defaultOpen={true}
                        >
                            <SubMenu title="Buy">
                                {data.userRole?.canRequestBuy && (
                                    <MenuItem>
                                        Buy an Item <Link to="/buy/material" />
                                    </MenuItem>
                                )}
                                <MenuItem>
                                    Buy Requests <Link to="/buy" />
                                </MenuItem>
                            </SubMenu>
                            {data.userRole.canViewPurchase && (
                                <SubMenu title="Purchase">
                                    {data.userRole?.canRequestPurchase && (
                                        <MenuItem>
                                            Purchase an Item <Link to="/purchase/material" />
                                        </MenuItem>
                                    )}

                                    <MenuItem>
                                        List Purchase Requests <Link to="/purchase" />
                                    </MenuItem>
                                </SubMenu>
                            )}
                            {data.userRole.canViewReceive && (
                                <SubMenu title="Receive">
                                    {data.userRole?.canReceive && (
                                        <MenuItem>
                                            Receive an Item <Link to="/receive/material" />
                                        </MenuItem>
                                    )}

                                    <MenuItem>
                                        List Received Items <Link to="/receive" />
                                    </MenuItem>
                                </SubMenu>
                            )}
                            <SubMenu title="Transfer">
                                <MenuItem>
                                    New Material Transfer
                                    <Link to="/transfer/material" />
                                </MenuItem>
                                <MenuItem>
                                    New Equipment Transfer
                                    <Link to="/transfer/equipment" />
                                </MenuItem>
                                <MenuItem>
                                    Transfer In <Link to="/transfer/in" />
                                </MenuItem>
                                <MenuItem>
                                    Transfer Out
                                    <Link to="/transfer/out" />
                                </MenuItem>
                            </SubMenu>
                            <SubMenu title="Issue">
                                <MenuItem>
                                    New Issue Request
                                    <Link to="/issue/new" />
                                </MenuItem>
                                <MenuItem>
                                    Issue Requests
                                    <Link to="/issue" />
                                </MenuItem>
                            </SubMenu>
                            <SubMenu title="Borrow">
                                <MenuItem>
                                    New Borrow Request <Link to="/borrow/new" />
                                </MenuItem>
                                <MenuItem>
                                    Borrow Requests <Link to="/borrow" />
                                </MenuItem>
                                <MenuItem>
                                    Retrun Assets <Link to="/return/new" />
                                </MenuItem>
                                <MenuItem>
                                    Returned Assets <Link to="/return" />
                                </MenuItem>
                            </SubMenu>

                            <SubMenu title="Maintenance">
                                <MenuItem>
                                    New Maintenance Request <Link to="/maintenance/new" />
                                </MenuItem>
                                <MenuItem>
                                    Maintenance Requests <Link to="/maintenance" />
                                </MenuItem>
                            </SubMenu>
                            <SubMenu title="Rent">
                                <MenuItem>
                                    New Rent Request
                                    <Link to="/rent/new" />
                                </MenuItem>
                            </SubMenu>
                        </SubMenu>
                    </Menu>
                </SidebarContent>
                <SidebarFooter>
                    <Menu subMenuBullets={true}>
                        <SubMenu title={data.username} icon={<FaUser className="fs-5" />}>
                            <MenuItem>
                                Manage Account <Link to="/user/manage" />
                            </MenuItem>
                            <MenuItem
                                onClick={() => {
                                    localStorage.removeItem("token");
                                    localStorage.removeItem("data");
                                    updateAuthContext({ token: "", data: "", username: "" });
                                    navigate("/login");
                                }}
                            >
                                Logout
                            </MenuItem>
                        </SubMenu>
                    </Menu>
                </SidebarFooter>
            </ProSidebar>
        </>
    );
}

export default Sidebar;
