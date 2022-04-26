import PropTypes from "prop-types";
import { Button } from "react-bootstrap";
import IssueViewMaterial from "./fragments/BorrowViewEquipment";
import { useNavigate } from "react-router-dom";

function ViewBorrow({ addedItems }) {
    const navigate = useNavigate();

    function goBack() {
        navigate(-1);
    }

    return (
        <>
            {addedItems.map((item, index) => (
                <IssueViewMaterial
                    key={index}
                    addedItems={addedItems}
                    index={index}
                />
            ))}

            <div className="d-grid">
                <Button className="btn-teal" onClick={goBack}>
                    Back
                </Button>
            </div>
        </>
    );
}

ViewBorrow.propTypes = {
    addedItems: PropTypes.array.isRequired,
};

export default ViewBorrow;
