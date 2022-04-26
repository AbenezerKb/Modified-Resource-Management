import api from "./api";

export const fetchRoles = () => api.get("/userRole").then((res) => res.data);

export const addRole = (data) => api.post("/userRole/add", data);
