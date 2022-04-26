import Header from "../layouts/Header";
import LoadingSpinner from "../fragments/LoadingSpinner";
import { Container, Row, Col } from "react-bootstrap";
import NotificationDashboard from "../notification/NotificationDashboard";
import DashboardCard from "../fragments/DashboardCard";
import maintenanceImg from "../../icons/maintenance.jpg";
import issueImg from "../../icons/issue.jpg";
import borrowImg from "../../icons/borrow.jpg";
import transferImg from "../../icons/transfer.jpg";

function Dashboard() {
    return (
        <>
            <Header title="Home" showNotification={false} />
            <Container fluid>
                <Row>
                    <Col>
                        <Row className="g-4 mt-2 mx-3" xs={3} xl={4}>
                            <Col>
                                <DashboardCard
                                    img={issueImg}
                                    title="Issue"
                                    links={[
                                        {
                                            to: "/issue/new",
                                            label: "New Issue Request",
                                        },
                                        { to: "/issue", label: "View Issue Requests" },
                                    ]}
                                />
                            </Col>
                            <Col>
                                <DashboardCard
                                    img={borrowImg}
                                    title="Borrow"
                                    links={[
                                        {
                                            to: "/borrow/new",
                                            label: "New Borrow Request",
                                        },
                                        { to: "/borrow", label: "View Borrow Requests" },
                                        {
                                            to: "/return/new",
                                            label: "New Return Request",
                                        },
                                        { to: "/return", label: "View Returns" },
                                    ]}
                                />
                            </Col>
                            <Col>
                                <DashboardCard
                                    img={transferImg}
                                    title="Transfer"
                                    links={[
                                        {
                                            to: "/transfer/material",
                                            label: "New Material Transfer",
                                        },
                                        {
                                            to: "/transfer/equipment",
                                            label: "New Equipment Transfer",
                                        },
                                        {
                                            to: "/transfer/in",
                                            label: "View Transfer In Requests",
                                        },
                                        {
                                            to: "/transfer/out",
                                            label: "View Transfer Out Requests",
                                        },
                                    ]}
                                />
                            </Col>
                            <Col>
                                <DashboardCard
                                    img={maintenanceImg}
                                    title="Maintenance"
                                    links={[
                                        {
                                            to: "/maintenance/new",
                                            label: "New Maintenance Request",
                                        },
                                        {
                                            to: "/maintenance",
                                            label: "View Maintenance Requests",
                                        },
                                    ]}
                                />
                            </Col>
                            <Col>
                                <DashboardCard
                                    img={maintenanceImg}
                                    title="Purchase"
                                    links={[
                                        {
                                            to: "/purchase/material",
                                            label: "Buy Material Request",
                                        },
                                        {
                                            to: "/purchase",
                                            label: "View Purchase Requests",
                                        },
                                    ]}
                                />
                            </Col>
                            <Col>
                                <DashboardCard
                                    img={maintenanceImg}
                                    title="Buy"
                                    links={[
                                        {
                                            to: "/buy/material",
                                            label: "Purchase Material Request",
                                        },
                                        { to: "/buy", label: "View Buy Requests" },
                                    ]}
                                />
                            </Col>
                            <Col>
                                <DashboardCard
                                    img={maintenanceImg}
                                    title="Receive"
                                    links={[
                                        {
                                            to: "/receive/material",
                                            label: "Receive Material Request",
                                        },
                                        { to: "/receive", label: "View Receives" },
                                    ]}
                                />
                            </Col>
                        </Row>
                    </Col>
                    <Col xs={12} lg={4} className="pe-0">
                        <NotificationDashboard />
                    </Col>
                </Row>
            </Container>
        </>
    );
}

export default Dashboard;
