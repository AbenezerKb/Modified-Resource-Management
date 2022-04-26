import React from "react";
import PropTypes from "prop-types";
import PurchaseCheckMaterial from "./fragments/PurchaseCheckMaterial";
import { Button, Spinner } from "react-bootstrap";
import { checkPurchase, declinePurchase } from "../../../api/purchase";
import { useMutation, useQueryClient } from "react-query";
import ConnectionError from "../../fragments/ConnectionError";

function CheckPurchase({ addedItems, setAddedItems, purchase }) {
    const queryClient = useQueryClient();

    const {
        isLoading: isCheckLoading,
        isError: isCheckError,
        mutate: submitCheckPurchase,
    } = useMutation(checkPurchase, {
        onSuccess: (res) =>
            queryClient.invalidateQueries(["purchase", purchase.purchaseId.toString()]),
    });

    const {
        isLoading: isDeclineLoading,
        isError: isDeclineError,
        mutate: submitDeclinePurchase,
    } = useMutation(declinePurchase, {
        onSuccess: (res) =>
            queryClient.invalidateQueries(["purchase", purchase.purchaseId.toString()]),
    });


    function submit(type, e) {
        e.preventDefault();
        if (isCheckLoading || isDeclineLoading) return;

        const data = {
            purchaseId: purchase.purchaseId,
            purchaseItems: [],
        };

        for (let item of addedItems) {
            let tempItem = {
                itemId: Number(item.itemId),
                qtyApproved: Number(item.qtyApproved),
                approveRemark: item.approveRemark,
            };

            data.purchaseItems.push(tempItem);
        }

        if (type === "decline") submitDeclinePurchase(data);
        else submitCheckPurchase(data);
    }

    if (isCheckError || isDeclineError) return <ConnectionError />;

    return (
        <>
            {addedItems.map((item, index) => (
                <PurchaseCheckMaterial
                    key={index}
                    addedItems={addedItems}
                    setAddedItems={setAddedItems}
                    index={index}
                />
            ))}

            <div className="row">
                <div className="col-8 d-grid">
                    <Button className="btn-teal-dark" onClick={submit.bind(this, "check")}>
                        {isCheckLoading && (
                            <Spinner className="me-2" animation="grow" size="sm" />
                        )}
                        Check
                    </Button>
                </div>
                <div className="col-4 d-grid">
                    <Button variant="danger" onClick={submit.bind(this, "decline")}>
                        {isDeclineLoading && (
                            <Spinner className="me-2" animation="grow" size="sm" />
                        )}
                        Decline
                    </Button>
                </div>
            </div>
        </>
    );
}

CheckPurchase.propTypes = {
    addedItems: PropTypes.array.isRequired,
    setAddedItems: PropTypes.func.isRequired,
    purchase: PropTypes.object.isRequired,
};

export default CheckPurchase;
