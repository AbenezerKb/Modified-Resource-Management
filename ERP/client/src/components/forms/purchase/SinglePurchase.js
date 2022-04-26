import { useEffect, useState, useMemo } from "react";
import { useParams } from "react-router-dom";
import { Form, Container } from "react-bootstrap";
import Item from "../../../models/Item";
import Header from "../../layouts/Header";
import PurchaseStatusBadge from "./fragments/PurchaseStatusBadge";
import { ITEMTYPE, PURCHASESTATUS } from "../../../Constants";
import ApprovePurchase from "./ApprovePurchase";
import ConfirmPurchase from "./ConfirmPurchase";
import RequestQueuedPurchase from "./RequestQueuedPurchase";
import CheckPurchase from "./CheckPurchase";
import ViewPurchase from "./ViewPurchase";
import DeclinedPurchase from "./DeclinedPurchase";
import { useQuery } from "react-query";
import { fetchPurchase } from "../../../api/purchase";
import { useAuth } from "../../../contexts/AuthContext";
import LoadingSpinner from "../../fragments/LoadingSpinner";
import ConnectionError from "../../fragments/ConnectionError";

function SinglePurchase() {
    const { id } = useParams();
    const {data} = useAuth();
    const [purchase, setPurchase] = useState({});
    const [addedItems, setAddedItems] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    //const employee = useSelector(state=>state.auth.data[1])

    var query = useQuery(["purchase", id], () => fetchPurchase(id));

    useEffect(() => {
        if (query.data === undefined) return;
        setPurchase(query.data);
        let items = [];
        for (let purchaseItem of query.data.purchaseItems) {
            let itemObj = new Item();

            itemObj.itemId = purchaseItem.itemId;
            itemObj.name = purchaseItem.item.name;
            itemObj.cost = purchaseItem.cost;
            itemObj.type = purchaseItem.item.type

            itemObj.qtyRequested = purchaseItem.qtyRequested;
            itemObj.requestRemark = purchaseItem.requestRemark;

            if (
                query.data.status >= PURCHASESTATUS.APPROVED ||
                query.data.status === PURCHASESTATUS.DECLINED
            ) {
                itemObj.qtyApproved = purchaseItem.qtyApproved;
                itemObj.approveRemark = purchaseItem.approveRemark;
            }

            if (
                query.data.status >= PURCHASESTATUS.PURCHASED
            ) {
                itemObj.qtyPurchased = purchaseItem.qtyPurchased;
                itemObj.purchaseRemark = purchaseItem.purchaseRemark;
            }

            if (purchaseItem.item.type === ITEMTYPE.MATERIAL) {
                itemObj.spec = purchaseItem.item.material.spec;
                itemObj.units = [purchaseItem.item.material.unit];
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
                    labelL="Purchase Number"
                    valueL={purchase.purchaseId}
                    labelR="Status"
                    valueR={PurchaseStatusBadge(purchase.status)}
                />

                <FormRow
                    labelL="Receiving Site"
                    valueL={purchase.receivingSite.name}
                />

                <FormRow
                    labelL="Requested By"
                    valueL={`${purchase.requestedBy.fName} ${purchase.requestedBy.mName}`}
                    labelR="Requested On"
                    valueR={new Date(purchase.requestDate).toLocaleString()}
                />
                {purchase.status >= PURCHASESTATUS.CHECKED && (
                    <>
                        <FormRow
                            labelL="Checked By"
                            valueL={
                                purchase.checkedBy
                                    ? `${purchase.checkedBy.fName} ${purchase.checkedBy.mName}`
                                    : ""
                            }
                            labelR="Checked On"
                            valueR={
                                purchase.checkDate
                                    ? new Date(purchase.checkDate).toLocaleString()
                                    : ""
                            }
                        />
                    </>
                )}
                {purchase.status >= PURCHASESTATUS.APPROVED && (
                    <>
                        <FormRow
                            labelL="Approved By"
                            valueL={
                                purchase.approvedBy
                                    ? `${purchase.approvedBy.fName} ${purchase.approvedBy.mName}`
                                    : ""
                            }
                            labelR="Approved On"
                            valueR={
                                purchase.approveDate
                                    ? new Date(purchase.approveDate).toLocaleString()
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
            "Declined Purchase",
            "Request Purchase",
            "Check Purchase",
            "Approve Purchase",
            "Confirm Purchase",
            "Item Purchased"
        ],
        []
    );

    if (query.isError) return <ConnectionError />;

    if (isLoading) return <LoadingSpinner />;

    return (
        <>
            <Header title={titles[purchase.status]} />

            <Container className="my-3">
                <>
                    <TopForm />

                    {purchase.status === PURCHASESTATUS.DECLINED && (
                        <DeclinedPurchase
                            addedItems={addedItems}
                            FormRow={FormRow}
                            purchase={purchase}
                        />
                    )}

                    {purchase.status === PURCHASESTATUS.QUEUED && (
                        <RequestQueuedPurchase
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            purchase={purchase}
                        />
                    )}

                    {purchase.status === PURCHASESTATUS.REQUESTED && data.userRole.canCheckPurchase && (
                        <CheckPurchase
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            purchase={purchase}
                        />
                    )}

                    {purchase.status === PURCHASESTATUS.CHECKED && data.userRole.canApprovePurchase && (
                        <ApprovePurchase
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            purchase={purchase}
                        />
                    )}

                    {purchase.status === PURCHASESTATUS.APPROVED && data.userRole.canConfirmPurchase &&(
                        <ConfirmPurchase
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            purchase={purchase}
                        />
                    )}

                    {purchase.status === PURCHASESTATUS.PURCHASED && (
                        <ViewPurchase addedItems={addedItems} purchase={purchase} />
                    )}
                </>
            </Container>
        </>
    );
}

export default SinglePurchase;