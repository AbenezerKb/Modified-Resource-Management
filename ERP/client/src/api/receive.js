import api from "./api";

export const fetchReceives = () =>
    api.get("/receive").then((res) => res.data);

export const fetchReceive = (id) =>
    api.get(`/receive/${id}`).then((res) => res.data);

export const fetchMySiteReceives = () =>
    api.get(`/receive/mysite`).then((res) => res.data);

export const requestReceive = (data) => api.post("/receive/request", data);

export const approveReceive = (data) => api.post("/receive/approve", data);
