import PropTypes from "prop-types";
import { Button } from "react-bootstrap";
import ReturnViewEquipment from "./fragments/ReturnViewEquipment";
import { useNavigate } from "react-router-dom";

function ViewReturn({ addedItems }) {
    const navigate = useNavigate();

    function goBack() {
        navigate(-1);
    }

    return (
        <>
            {addedItems.map((item, index) => (
                <ReturnViewEquipment key={index} addedItems={addedItems} index={index} />
            ))}

            <div className="d-grid">
                <Button className="btn-teal" onClick={goBack}>
                    Back
                </Button>
            </div>
        </>
    );
}

ViewReturn.propTypes = {
    addedItems: PropTypes.array.isRequired,
};

export default ViewReturn;
