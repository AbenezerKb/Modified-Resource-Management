import React from "react";
import PropTypes from "prop-types";
import ReceiveApprove from "./fragments/ReceiveApprove";
import { Button, Spinner } from "react-bootstrap";
import { approveReceive } from "../../../api/receive";
import { useMutation, useQueryClient } from "react-query";
import ConnectionError from "../../fragments/ConnectionError";

function ApproveReceive({ addedItems, setAddedItems, receive }) {
    const queryClient = useQueryClient();

    const {
        isLoading: isApproveLoading,
        isError: isApproveError,
        mutate: submitApproveReceive,
    } = useMutation(approveReceive, {
        onSuccess: (res) =>
            queryClient.invalidateQueries(["receive", receive.receiveId.toString()]),
    });

    function submit(type, e) {
        e.preventDefault();
        if (isApproveLoading) return;

        const data = {
            receiveId: receive.receiveId,
            receiveItems: [],
        };
        
        for (let item of addedItems) {
            let tempItem = {
                itemId: Number(item.itemId),
                qtyReceived: Number(item.qtyReceived),
                receiveRemark: item.receiveRemark,
            };

            data.receiveItems.push(tempItem);
        }

        submitApproveReceive(data);
    }

    if (isApproveError) return <ConnectionError />;

    return (
        <>
                {addedItems.map((item, index) => (
                    <ReceiveApprove
                        key={index}
                        addedItems={addedItems}
                        setAddedItems={setAddedItems}
                        index={index}
                    />
                ))}

                <div className="row">
                    <div className="d-grid">
                        <Button className="btn-teal-dark" onClick={submit.bind(this, "approve")}>
                            {isApproveLoading && (
                                <Spinner className="me-2" animation="grow" size="sm" />
                            )}
                            Approve
                        </Button>
                    </div>
                </div>
        </>
    );
}

ApproveReceive.propTypes = {
    addedItems: PropTypes.array.isRequired,
    setAddedItems: PropTypes.func.isRequired,
    receive: PropTypes.object.isRequired,
};

export default ApproveReceive;
