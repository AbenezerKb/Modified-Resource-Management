import React from "react";
import { useEffect, useState, useMemo } from "react";
import { useParams } from "react-router-dom";
import { Form, Button, Container } from "react-bootstrap";
import { FaEdit } from "react-icons/fa";
import api from "../../../api/api";

import Header from "../../layouts/Header";
import EmployeeStatusBadge from "./fragments/EmployeeStatusBadge";
import { EMPLOYEESTATUS } from "../../../Constants";
import { ToastContainer, toast } from "react-toastify";
import { useQuery, useMutation } from "react-query";
import { fetchEmployee, updateEmployee } from "../../../api/employee";
import { useAuth } from "../../../contexts/AuthContext";
import LoadingSpinner from "../../fragments/LoadingSpinner";
import ConnectionError from "../../fragments/ConnectionError";

function SingleEmployee() {
    const { id } = useParams();

    const [fName, setFName] = useState("");
    const [mName, setMName] = useState("");
    const [lName, setLName] = useState("");
    const [position, setPosition] = useState("");
    const [site, setSite] = useState(0);
    const [status, setStatus] = useState(0);
    const [role, setRole] = useState(0);
    const [employee, setEmployee] = useState({});

    const [allUserRole, setAllUserRole] = useState([]);
    const [allSite, setAllSite] = useState([]);

    const [isLoading, setIsLoading] = useState(true);

    var query = useQuery(["employee", id], () => fetchEmployee(id));

    const toastOption = {
        position: "bottom-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        theme: "dark",
        // progress: undefined
    };

    useEffect(() => {
        if (query.data === undefined) return;
        setEmployee(query.data);
        setFName(query.data.fName);
        setMName(query.data.mName);
        setLName(query.data.lName);
        setPosition(query.data.position);
        setRole(query.data.userRoleId);
        setSite(query.data.employeeSiteId);
        setStatus(query.data.status);
        setIsLoading(false);
    }, [query.data]);

    useEffect(() => {
        async function fetchRole() {
            const res = await api.get("/UserRole");
            setAllUserRole(res.data);
        }
        fetchRole();
        async function fetchSite() {
            const res = await api.get("/site");
            setAllSite(res.data);
        }
        fetchSite();
    }, []);

    const titles = useMemo(() => ["Registered Employee", "Approved Employee"], []);

    const { mutate: submitUpdateEmployee } = useMutation(updateEmployee, {
        onSuccess: () => {
            toast.success("Employee Data Is Successfully Updated", toastOption);
        },
    });

    function submit(e) {
        e.preventDefault();

        var data = {
            employeeId: employee.employeeId,
            status: Number(status),
            fName: String(fName),
            mName: String(mName),
            lName: String(lName),
            position: String(position),
            employeeSiteId: Number(site),
            userRoleId: Number(role),
        };

        submitUpdateEmployee(data);
    }

    if (isLoading) return <LoadingSpinner />;

    if (query.isError) return <ConnectionError />;

    return (
        <>
            <Header title={titles[employee.status]} />

            <Container className="my-3 align-self-center">
                <Form onSubmit={submit}>
                    <div className="col-10 mx-auto shadow py-5 px-5 rounded">
                        <div className="row ">
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>First Name</Form.Label>
                                    <Form.Control
                                        type="text"
                                        value={fName}
                                        onChange={(e) => setFName(e.target.value)}
                                    />
                                </Form.Group>
                            </div>
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Middle Name</Form.Label>
                                    <Form.Control
                                        type="text"
                                        value={mName}
                                        onChange={(e) => setMName(e.target.value)}
                                    />
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Last Name</Form.Label>
                                    <Form.Control
                                        type="text"
                                        value={lName}
                                        onChange={(e) => setLName(e.target.value)}
                                    />
                                </Form.Group>
                            </div>
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Working Site</Form.Label>
                                    <Form.Select
                                        value={site}
                                        onChange={(e) => setSite(e.target.value)}
                                    >
                                        {allSite.map((site) => (
                                            <option key={site.siteId} value={site.siteId}>
                                                {site.name}
                                            </option>
                                        ))}
                                    </Form.Select>
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Position</Form.Label>
                                    <Form.Control
                                        type="text"
                                        value={position}
                                        onChange={(e) => setPosition(e.target.value)}
                                    />
                                </Form.Group>
                            </div>
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>User Role</Form.Label>
                                    <Form.Select
                                        value={role}
                                        onChange={(e) => setRole(e.target.value)}
                                    >
                                        {allUserRole.map((role) => (
                                            <option key={role.roleId} value={role.roleId}>
                                                {role.role}
                                            </option>
                                        ))}
                                    </Form.Select>
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col-6">
                                <Form.Group className="mb-3">
                                    <Form.Check
                                        inline
                                        label="Approved"
                                        checked={status}
                                        onChange={(e) => setStatus(e.target.checked)}
                                        type="checkbox"
                                    ></Form.Check>
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row">
                            <div className="d-grid">
                                <Button type="submit" className="btn-teal">
                                    <FaEdit className="me-2 mb-1" /> Update Employee Data
                                </Button>
                            </div>
                        </div>
                    </div>
                </Form>
                <ToastContainer />
            </Container>
        </>
    );
}

export default SingleEmployee;
