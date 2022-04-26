import { useEffect, useState, useMemo } from "react";
import { useParams } from "react-router-dom";
import { Form, Container } from "react-bootstrap";
import Item from "../../../models/Item";
import Header from "../../layouts/Header";
import ReceiveStatusBadge from "./fragments/ReceiveStatusBadge";
import { ITEMTYPE, RECEIVESTATUS } from "../../../Constants";
import ApproveReceive from "./ApproveReceive";
import MaterialReceive from "./MaterialReceive";
import ViewReceive from "./ViewReceive";
import { useQuery } from "react-query";
import { fetchReceive } from "../../../api/receive";
import LoadingSpinner from "../../fragments/LoadingSpinner";
import ConnectionError from "../../fragments/ConnectionError";

function SingleReceive() {
    const { id } = useParams();
    const [receive, setReceive] = useState({});
    const [addedItems, setAddedItems] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    var query = useQuery(["receive", id], () => fetchReceive(id));

    useEffect(() => {
        if (query.data === undefined) return;
        setReceive(query.data);
        let items = [];
        for (let receiveItem of query.data.receiveItems) {
            let itemObj = new Item();

            itemObj.itemId = receiveItem.itemId;
            itemObj.name = receiveItem.item.name;
            itemObj.type = receiveItem.item.type

            itemObj.qtyPurchased = receiveItem.qtyPurchased;

            if (
                query.data.status >= RECEIVESTATUS.RECEIVED
            ) {
                itemObj.qtyReceived = receiveItem.qtyReceived;
                itemObj.receiveRemark = receiveItem.receiveRemark;
            }

            if (receiveItem.item.type === ITEMTYPE.MATERIAL) {
                itemObj.spec = receiveItem.item.material.spec;
                itemObj.units = [receiveItem.item.material.unit];
            }

            items.push(itemObj);
        }

        setAddedItems(items);
        setIsLoading(false);
    }, [query.data]);

    function FormRow({ labelL, labelR, valueL, valueR }) {
        //defines layout of each row
        return (
            <div className="row mx-2">
                <div className="col">
                    <Form.Group className="mb-3">
                        <div className="row">
                            <div className="col-3 mt-1">
                                <Form.Label>{labelL}</Form.Label>
                            </div>
                            <div className="col">
                                {typeof valueL === "object" ? (
                                    valueL
                                ) : (
                                    <Form.Control type="text" readOnly value={valueL} />
                                )}
                            </div>
                        </div>
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className="mb-3">
                        <div className="row">
                            <div className="col-3 mt-1">
                                <Form.Label>{labelR}</Form.Label>
                            </div>
                            <div className="col">
                                {typeof valueR === "object" ? (
                                    valueR
                                ) : (
                                    <Form.Control type="text" readOnly value={valueR} />
                                )}
                            </div>
                        </div>
                    </Form.Group>
                </div>
            </div>
        );
    }

    function TopForm() {
        return (
            <Form>
                <FormRow
                    labelL="Receive Number"
                    valueL={receive.receiveId}
                    labelR="Status"
                    valueR={ReceiveStatusBadge(receive.status)}
                />

                {receive.status >= RECEIVESTATUS.APPROVED && (
                    <>
                        <FormRow
                            labelL="Approved By"
                            valueL={
                                receive.approvedBy
                                    ? `${receive.approvedBy.fName} ${receive.approvedBy.mName}`
                                    : ""
                            }
                            labelR="Approved On"
                            valueR={
                                receive.approveDate
                                    ? new Date(receive.approveDate).toLocaleString()
                                    : ""
                            }
                        />
                        <FormRow
                            labelL="Approve Remark"
                            valueL={
                                receive.approveRemark
                                    ? `${receive.approveRemark}`
                                    : ""
                            }
                        />
                    </>
                )}
            </Form>
        );
    }

    const titles = useMemo(
        () => [
            "",
            "Receive Item",
            "Approve Receive",
            "Approved Receive",
        ],
        []
    );

    if (isLoading) return <LoadingSpinner />;

    if (query.isError) return <ConnectionError />;

    return (
        <>
            <Header title={titles[receive.status]} />

            <Container className="my-3">
                <>
                    <TopForm />

                    {receive.status === RECEIVESTATUS.PURCHASED && (
                        <MaterialReceive
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            receive={receive}
                        />
                    )}

                    {/* {receive.status === RECEIVESTATUS.PURCHASED && (
                        <ApproveReceive
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            receive={receive}
                        />
                    )} */}

                    {receive.status === RECEIVESTATUS.RECEIVED && (
                        <ViewReceive addedItems={addedItems} receive={receive} />
                    )}
                </>
            </Container>
        </>
    );
}

export default SingleReceive;
