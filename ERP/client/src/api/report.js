import api from "./api";

export const fetchTransferReport = (data) =>
    api.post("/report/transfer", data).then((res) => res.data);

export const fetchIssueReport = (data) => api.post("/report/issue", data).then((res) => res.data);

export const fetchGeneralReport = (data) => api.post("/aa", data).then((res) => res.data);

export const fetchBorrowReport = (data) => api.post("/report/borrow", data).then((res) => res.data);

export const fetchMaintenanceReport = (data) =>
    api.post("/report/maintenance", data).then((res) => res.data);
