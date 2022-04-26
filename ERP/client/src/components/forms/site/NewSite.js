import { useState } from "react";
import { Form, Button, Container } from "react-bootstrap";
import { FaPlus } from "react-icons/fa";
import Header from "../../layouts/Header";
import { ToastContainer, toast } from "react-toastify";
import LoadingSpinner from "../../fragments/LoadingSpinner";
import ConnectionError from "../../fragments/ConnectionError";
import AlertNotification from "../../fragments/AlertNotification";

function NewSite() {
    const [name, setName] = useState("");
    const [location, setLocation] = useState("");
    const [pettyCashLimit, setPettyCashLimit] = useState(1000);

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

    function submit(e) {
        toast.success("Site Has Been Successfully Added", toastOption);
    }

    return (
        <>
            <Header title="Add new Site" />
            <Container className="my-3 align-self-center">
                <AlertNotification
                    title="Feature Under Construction"
                    content="Adding Site Feature is Under Construction. It Will Be Available Once Development Is Completed."
                />
                <Form onSubmit={submit}>
                    <div className="col-10 mx-auto shadow py-5 px-5 rounded">
                        <div className="row ">
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Site Name</Form.Label>
                                    <Form.Control
                                        type="text"
                                        value={name}
                                        onChange={(e) => setName(e.target.value)}
                                    />
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Location</Form.Label>
                                    <Form.Control
                                        type="text"
                                        value={location}
                                        onChange={(e) => setLocation(e.target.value)}
                                    />
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col">
                                <Form.Group className="mb-3">
                                    <Form.Label>Petty Cash Limit</Form.Label>
                                    <Form.Control
                                        type="number"
                                        value={pettyCashLimit}
                                        onChange={(e) =>
                                            setPettyCashLimit(e.target.value)
                                        }
                                    />
                                </Form.Group>
                            </div>
                        </div>

                        <div className="row">
                            <div className="d-grid">
                                <Button type="submit" className="btn-teal">
                                    <FaPlus className="me-2 mb-1" /> Add Site
                                </Button>
                            </div>
                        </div>
                    </div>
                </Form>
                <ToastContainer />
            </Container>
            <ToastContainer />
        </>
    );
}

export default NewSite;
