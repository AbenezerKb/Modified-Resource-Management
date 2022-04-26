import React, { useRef } from "react";
import PropTypes from "prop-types";
import { Button } from "react-bootstrap";
import PurchaseViewMaterial from "./fragments/PurchaseViewMaterial";
import { useNavigate } from "react-router-dom";
import { ITEMTYPE } from "../../../Constants";

function ViewPurchase({ addedItems }) {
    const navigate = useNavigate();

    function goBack() {
        navigate(-1);
    }

    return (
        <>
            {addedItems.map((item, index) =>
                item.type === ITEMTYPE.MATERIAL ? (
                    <PurchaseViewMaterial key={index} addedItems={addedItems} index={index} />
                ) : (
                    <PurchaseViewMaterial key={index} addedItems={addedItems} index={index} />
                )
            )}

            <div className="d-grid">
                <Button className="btn-teal" onClick={goBack}>
                    Back
                </Button>
            </div>
        </>
    );
}

ViewPurchase.propTypes = {
    addedItems: PropTypes.array.isRequired,
};

export default ViewPurchase;
