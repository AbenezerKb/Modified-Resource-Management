import React, { useRef } from "react";
import PropTypes from "prop-types";
import { Button } from "react-bootstrap";
import ReceiveView from "./fragments/ReceiveView";
import { useNavigate } from "react-router-dom";
import { ITEMTYPE } from "../../../Constants";

function ViewReceive({ addedItems }) {
    const navigate = useNavigate();

    function goBack() {
        navigate(-1);
    }

    return (
        <>
            {addedItems.map((item, index) =>
                item.type === ITEMTYPE.MATERIAL ? (
                    <ReceiveView key={index} addedItems={addedItems} index={index} />
                ) : (
                    <ReceiveView key={index} addedItems={addedItems} index={index} />
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

ViewReceive.propTypes = {
    addedItems: PropTypes.array.isRequired,
};

export default ViewReceive;
