import { Form, InputGroup, Table } from "react-bootstrap";
import PropTypes from "prop-types";
import { useQuery } from "react-query";
import { fetchEquipmentModel } from "../../../../api/item";

function ReturnViewEquipment({ addedItems, index }) {
    var modelQuery = useQuery(
        ["model", addedItems[index].equipmentModelId],
        () =>
            addedItems[index].equipmentModelId &&
            fetchEquipmentModel(addedItems[index].equipmentModelId)
    );

    return (
        <div className="shadow pt-3 px-4 mb-4 rounded">
            <div className=" row justify-content-between">
                <div className="col-auto">
                    <h1 className="display-6 fs-4">Equipment {index + 1}</h1>
                </div>
            </div>

            <div className="row">
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Borrowed By</Form.Label>
                        <Form.Control
                            type="text"
                            value={`${addedItems[index].requestedBy.fName} ${addedItems[index].requestedBy.mName}`}
                            readOnly
                        />
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Handed Over On</Form.Label>
                        <Form.Control
                            type="text"
                            value={new Date(addedItems[index].handDate).toLocaleString()}
                            readOnly
                        />
                    </Form.Group>
                </div>
            </div>

            <div className="row">
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Item</Form.Label>
                        <Form.Control type="text" readOnly value={addedItems[index].name} />
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Model</Form.Label>
                        <Form.Control type="text" value={modelQuery?.data?.name || ""} readOnly />
                    </Form.Group>
                </div>
            </div>
            <div className="row">
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Asset Number</Form.Label>
                        <Form.Control
                            type="text"
                            value={addedItems[index].equipmentAssets[0].assetNo}
                            readOnly
                        />
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Serial Number</Form.Label>
                        <Form.Control
                            type="text"
                            value={addedItems[index].equipmentAssets[0].serialNo}
                            readOnly
                        />
                    </Form.Group>
                </div>
            </div>

            <div className="row">
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Damage</Form.Label>
                        <Form.Control type="text" value={addedItems[index].assetDamage} readOnly />
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className="mb-3">
                        <Form.Label>Return Remark</Form.Label>
                        <Form.Control type="text" value={addedItems[index].returnRemark} readOnly />
                    </Form.Group>
                </div>
            </div>
        </div>
    );
}

ReturnViewEquipment.propTypes = {
    addedItems: PropTypes.array.isRequired,
    index: PropTypes.number.isRequired,
};

export default ReturnViewEquipment;
