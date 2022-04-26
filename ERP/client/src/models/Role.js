class Role {
    constructor() {
        this.Id = 0;
        this.Role = "";
        this.CanRequestBorrow = false
        this.CanApproveBorrow = false
        this.CanHandBorrow = false
        this.CanReturnBorrow = false
        this.CanViewBorrow = false

        this.CanRequestBuy = false
        this.CanCheckBuy = false
        this.CanApproveBuy = false
        this.CanConfirmBuy = false
        this.CanViewBuy = false

        this.CanReceive = false
        this.CanApproveReceive = false
        this.CanViewReceive = false

        this.CanApproveUser = false
        this.CanDeleteUser = false

        this.CanRequestPurchase = false
        this.CanCheckPurchase = false
        this.CanApprovePurchase = false
        this.CanConfirmPurchase = false
        this.CanViewPurchase = false

        this.CanFixMaintenance = false
        this.CanApproveMaintenance = false
        this.CanRequestMaintenance = false
        this.CanViewMaintenance = false

        this.CanRequestIssue = false
        this.CanApproveIssue = false
        this.CanHandIssue = false
        this.CanViewIssue = false

        this.CanRequestTransfer = false
        this.CanApproveTransfer = false
        this.CanReceiveTransfer = false
        this.CanSendTransfer = false
        this.CanViewTransfer = false

        this.CanGetStockNotification
    }

}

export default Role;
