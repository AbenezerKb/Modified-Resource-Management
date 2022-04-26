import React, { useRef } from "react";
import PropTypes from "prop-types";
import { Button } from "react-bootstrap";
import BuyViewMaterial from "./fragments/BuyViewMaterial";
import { useNavigate } from "react-router-dom";
import { ITEMTYPE } from "../../../Constants";

function ViewBuy({ addedItems }) {
    const navigate = useNavigate();

    function goBack() {
        navigate(-1);
    }

    return (
        <>
            {addedItems.map((item, index) =>
                item.type === ITEMTYPE.MATERIAL ? (
                    <BuyViewMaterial key={index} addedItems={addedItems} index={index} />
                ) : (
                    <BuyViewMaterial key={index} addedItems={addedItems} index={index} />
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

ViewBuy.propTypes = {
    addedItems: PropTypes.array.isRequired,
};

export default ViewBuy;
