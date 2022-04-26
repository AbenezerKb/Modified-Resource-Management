import React from "react";
import PropTypes from "prop-types";

function ConnectionError({ status }) {
    if (status === 401)
        return (
            <div>
                <h1 className="display-6 text-center mt-4 pt-4">
                    You Are Not Authorized To Access The Requested Data.
                </h1>
            </div>
        );
    return (
        <div>
            <h1 className="display-6 text-center mt-4 pt-4">Couldn't Connect, Please Try Again.</h1>
        </div>
    );
}

ConnectionError.propTypes = {
    status: PropTypes.number.isRequired,
};

export default ConnectionError;
