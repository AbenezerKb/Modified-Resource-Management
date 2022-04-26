import { useEffect, useState, useMemo } from "react";
import { useParams } from "react-router-dom";
import { Form, Container } from "react-bootstrap";
import Item from "../../../models/Item";
import Header from "../../layouts/Header";
import BuyStatusBadge from "./fragments/BuyStatusBadge";
import { ITEMTYPE, BUYSTATUS } from "../../../Constants";
import ApproveBuy from "./ApproveBuy";
import ConfirmBuy from "./ConfirmBuy";
import CheckBuy from "./CheckBuy";
import ViewBuy from "./ViewBuy";
import DeclinedBuy from "./DeclinedBuy";
import { useQuery } from "react-query";
import { fetchBuy } from "../../../api/buy";
import LoadingSpinner from "../../fragments/LoadingSpinner";
import ConnectionError from "../../fragments/ConnectionError";

function SingleBuy() {
    const { id } = useParams();
    const [buy, setBuy] = useState({});
    const [addedItems, setAddedItems] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    var query = useQuery(["buy", id], () => fetchBuy(id));

    useEffect(() => {
        if (query.data === undefined) return;
        setBuy(query.data);
        let items = [];
        for (let buyItem of query.data.buyItems) {
            let itemObj = new Item();

            itemObj.itemId = buyItem.itemId;
            itemObj.name = buyItem.item.name;
            itemObj.cost = buyItem.cost;
            itemObj.type = buyItem.item.type

            itemObj.qtyRequested = buyItem.qtyRequested;
            itemObj.requestRemark = buyItem.requestRemark;

            if (
                query.data.status >= BUYSTATUS.APPROVED ||
                query.data.status === BUYSTATUS.DECLINED
            ) {
                itemObj.qtyApproved = buyItem.qtyApproved;
                itemObj.approveRemark = buyItem.approveRemark;
            }

            if (
                query.data.status >= BUYSTATUS.BOUGHT
            ) {
                itemObj.qtyBought = buyItem.qtyBought;
                itemObj.buyRemark = buyItem.buyRemark;
            }

            if (buyItem.item.type === ITEMTYPE.MATERIAL) {
                itemObj.spec = buyItem.item.material.spec;
                itemObj.units = [buyItem.item.material.unit];
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
                    labelL="Buy Number"
                    valueL={buy.buyId}
                    labelR="Status"
                    valueR={BuyStatusBadge(buy.status)}
                />

                <FormRow
                    labelL="Requested By"
                    valueL={`${buy.requestedBy.fName} ${buy.requestedBy.mName}`}
                    labelR="Requested On"
                    valueR={new Date(buy.requestDate).toLocaleString()}
                />
                {buy.status >= BUYSTATUS.CHECKED && (
                    <>
                        <FormRow
                            labelL="Checked By"
                            valueL={
                                buy.checkedBy
                                    ? `${buy.checkedBy.fName} ${buy.checkedBy.mName}`
                                    : ""
                            }
                            labelR="Checked On"
                            valueR={
                                buy.checkDate
                                    ? new Date(buy.checkDate).toLocaleString()
                                    : ""
                            }
                        />
                    </>
                )}
                {buy.status >= BUYSTATUS.APPROVED && (
                    <>
                        <FormRow
                            labelL="Approved By"
                            valueL={
                                buy.approvedBy
                                    ? `${buy.approvedBy.fName} ${buy.approvedBy.mName}`
                                    : ""
                            }
                            labelR="Approved On"
                            valueR={
                                buy.approveDate
                                    ? new Date(buy.approveDate).toLocaleString()
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
            "Declined Buy",
            "Check Buy",
            "Queued Buy",
            "Approve Buy",
            "Confirm Buy",
            "Item Bought",
        ],
        []
    );

    if (query.isError) return <ConnectionError />;

    if (isLoading) return <LoadingSpinner />;

    return (
        <>
            <Header title={titles[buy.status]} />

            <Container className="my-3">
                <>
                    <TopForm />

                    {buy.status === BUYSTATUS.DECLINED && (
                        <DeclinedBuy
                            addedItems={addedItems}
                            FormRow={FormRow}
                            buy={buy}
                        />
                    )}

                    {buy.status === BUYSTATUS.REQUESTED && (
                        <CheckBuy
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            buy={buy}
                        />
                    )}

                    {buy.status === BUYSTATUS.CHECKED && (
                        <ApproveBuy
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            buy={buy}
                        />
                    )}

                    {buy.status === BUYSTATUS.APPROVED && (
                        <ConfirmBuy
                            addedItems={addedItems}
                            setAddedItems={setAddedItems}
                            buy={buy}
                        />
                    )}

                    {(buy.status === BUYSTATUS.BOUGHT || buy.status === BUYSTATUS.QUEUED ) && (
                        <ViewBuy addedItems={addedItems} buy={buy} />
                    )}
                </>
            </Container>
        </>
    );
}

export default SingleBuy;
