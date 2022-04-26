import api from "./api";

export const fetchSites = () => api.get("/site").then((res) => res.data);
