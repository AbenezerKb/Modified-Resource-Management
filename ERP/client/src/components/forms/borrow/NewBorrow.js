import { useEffect, useRef, useState } from "react";
import { Form, Button, Container } from "react-bootstrap";
import { FaPlus } from "react-icons/fa";
import Item from "../../../models/Item";
import BorrowRequestEquipment from "./fragments/BorrowRequestEquipment";
import Header from "../../layouts/Header";
import { useNavigate } from "react-router-dom";
import { useQuery, useMutation } from "react-query";
import { fetchSites } from "../../../api/site";
import { requestBorrow } from "../../../api/borrow";
import LoadingSpinner from "../../fragments/LoadingSpinner";
import ConnectionError from "../../fragments/ConnectionError";

function NewBorrow() {
    const navigate = useNavigate();
    const [addedItems, setAddedItems] = useState([new Item()]);

    const {
        isError: isSubmitError,
        error: submitError,
        isLoading: isSubmitLoading,
        mutate: submitBorrowRequest,
    } = useMutation(requestBorrow, {
        onSuccess: (res) => navigate(`/borrow/${res.data}`),
    });

    function submit(e) {
        e.preventDefault();
        if (isSubmitLoading) return;

        var data = {
            requestedById: 1, ////////////////////////////////////////////////////////////////////////////////
            borrowItems: [],
        };

        for (let item of addedItems) {
            let tempItem = {
                itemId: item.itemId,
                equipmentModelId: item.equipmentModelId,
                qtyRequested: item.qtyRequested,
                requestRemark: item.requestRemark,
            };

            data.borrowItems.push(tempItem);
        }

        submitBorrowRequest(data);
    }

    //scroll when Item is added
    var pageend = useRef(null);
    const [scroll, setScroll] = useState(false);

    useEffect(() => {
        if (scroll) {
            pageend.scrollIntoView();
            setScroll(false);
        }
    }, [scroll]);

    function addItem() {
        setAddedItems([...addedItems, new Item()]);
        setScroll(true);
    }

    if (isSubmitError) return <ConnectionError status={submitError?.response?.status} />;

    return (
        <>
            <Header title="New Borrow Request" />

            <Container className="my-3">
                <Form onSubmit={submit}>
                    {addedItems.map((item, index) => (
                        <BorrowRequestEquipment
                            key={item.key}
                            index={index}
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                        />
                    ))}

                    <div className="row">
                        <div className="col-9 d-grid">
                            <Button type="submit" className="btn-teal-dark">
                                Request Borrow
                            </Button>
                        </div>
                        <div className="col-3 d-grid">
                            <Button className="btn-teal" onClick={addItem}>
                                <FaPlus className="me-2 mb-1" /> Add Item
                            </Button>
                        </div>
                    </div>
                </Form>
                <div
                    style={{ float: "left", clear: "both" }}
                    ref={(el) => {
                        pageend = el;
                    }}
                ></div>
            </Container>
        </>
    );
}

export default NewBorrow;
