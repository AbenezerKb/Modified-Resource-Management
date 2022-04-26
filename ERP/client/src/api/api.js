import axios from "axios";

axios.defaults.headers.common["Authorization"] = "bearer " + localStorage.getItem("token");

export default axios.create({ baseURL: "https://localhost:7067/api/" });
