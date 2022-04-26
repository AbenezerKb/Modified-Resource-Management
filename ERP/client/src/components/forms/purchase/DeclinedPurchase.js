import React from "react";
import PropTypes from "prop-types";
import PurchaseDecliendMaterial from "./fragments/PurchaseDeclinedMaterial";

function DeclinedPurchase({ purchase, addedItems, FormRow }) {
    return (
        <>
            <FormRow
                labelL="Declined By"
                valueL={
                    purchase.approvedBy
                        ? `${purchase.approvedBy.fName} ${purchase.approvedBy.mName}`
                        : ""
                }
                labelR="Declined On"
                valueR={
                    purchase.approveDate 
                        ? new Date(purchase.approveDate).toLocaleString() 
                        : ""}
            />

            <h1 className="display-6 text-center mt-2 mb-4">
                Purchase Request Has Been Declined
            </h1>

            {addedItems.map((item, index) => (
                <PurchaseDecliendMaterial 
                    key={index} addedItems={addedItems} 
                    index={index} />
            ))}
        </>
    );
}

DeclinedPurchase.propTypes = {
    purchase: PropTypes.object.isRequired,
    addedItems: PropTypes.array.isRequired,
};

export default DeclinedPurchase;
