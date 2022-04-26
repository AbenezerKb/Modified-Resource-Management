import React from "react";
import PropTypes from "prop-types";
import PurchaseRequestQueuedMaterial from "./fragments/PurchaseRequestQueuedMaterial";
import { Button, Form, Spinner } from "react-bootstrap";
import { requestQueuedPurchase } from "../../../api/purchase";
import { useMutation, useQueryClient } from "react-query";
import ConnectionError from "../../fragments/ConnectionError";

function RequestQueuedPurchase({ addedItems, setAddedItems, purchase }) {
    const queryClient = useQueryClient();

    const {
        isLoading: isRequestLoading,
        isError: isRequestError,
        mutate: submitRequestQueuedPurchase,
    } = useMutation(requestQueuedPurchase, {
        onSuccess: (res) =>
            queryClient.invalidateQueries(["purchase", purchase.purchaseId.toString()]),
    });


    function submit(e) {
        e.preventDefault();
        if (isRequestLoading) return;

        const data = {
            purchaseId: purchase.purchaseId,
            purchaseItems: [],
        };

        for (let item of addedItems) {
            let tempItem = {
                itemId: Number(item.itemId),
                qtyRequested: Number(item.qtyRequested),
                requestRemark: item.requestRemark,
            };

            data.purchaseItems.push(tempItem);
        } 

        submitRequestQueuedPurchase(data);
    }

    if (isRequestError) return <ConnectionError />;

    return (
        <>
            <Form onSubmit={submit}>

                {addedItems.map((item, index) => (
                    <PurchaseRequestQueuedMaterial
                        key={index}
                        addedItems={addedItems}
                        setAddedItems={setAddedItems}
                        index={index}
                    />
                ))}

                <div className="d-grid">
                    <Button className="btn-teal-dark" type="submit">
                        {isRequestLoading && (
                            <Spinner 
                                className="me-2" 
                                animation="grow" 
                                size="sm" />
                        )}
                        Request Purchase
                    </Button>
                </div>
            </Form>
        </>
    );
}

RequestQueuedPurchase.propTypes = {
    addedItems: PropTypes.array.isRequired,
    setAddedItems: PropTypes.func.isRequired,
    purchase: PropTypes.object.isRequired,
};

export default RequestQueuedPurchase;
