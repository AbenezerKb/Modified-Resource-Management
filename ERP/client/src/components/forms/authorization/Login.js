import React, { useState } from "react"
import axios from "axios"
import { Link, useNavigate } from "react-router-dom"
import { FcFlowChart } from "react-icons/fc"
import { useQuery } from "react-query"
import { Form, Button } from "react-bootstrap"
import api from "../../../api/api"
import { fetchEmployees, initializeEmployee } from "../../../api/employee"
import { ToastContainer, toast } from "react-toastify"
import "react-toastify/dist/ReactToastify.css"
import { useAuth, useAuthUpdate } from "../../../contexts/AuthContext"

const Login = () => {
  const navigate = useNavigate()
  const contextData = useAuth()
  const updateAuthContext = useAuthUpdate()
  const [value, setValue] = useState({
    username: "",
    password: "",
  })
  const [password, setPassword] = useState("")

  var { data, isLoading, isError } = useQuery("employees", fetchEmployees)

  const toastOption = {
    position: "bottom-right",
    autoClose: 5000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    theme: "dark",
  }

  const handleLogin = async (e) => {
    e.preventDefault()
    if (!value.username) {
      toast.error("Please provide your username", toastOption)
    } else if (!value.password) {
      toast.error("Please provide your password", toastOption)
    } else {
      try {
        const res = await api.post("Authorization/login", value)
        updateAuthContext({
          ...contextData,
          token: res.data.token,
          data: res.data,
        })
        localStorage.setItem("token", res.data.token)
        localStorage.setItem("data", JSON.stringify(res.data))

        axios.defaults.headers.common["Authorization"] =
          "bearer " + res.data.token

        navigate("/")
      } catch (err) {
        toast.error("incorrect username or password", toastOption)
      }
    }
  }

  function submit(e) {
    var data = {
      password: String(password),
    }

    initializeEmployee(data)
    isLoading = true
    navigate("/login")
  }

  return (
    <div className="loginAndRegistration">
      {contextData?.username && (
        <div style={{ maxWidth: "600px", margin: "auto" }}>
          <div
            style={{ marginTop: "30px" }}
            className="alert alert-success"
            role="alert"
          >
            You have successfully registered. Use username:{" "}
            <b>{contextData.username}</b> to login.
          </div>
        </div>
      )}
      {!isLoading && !isError && data.length == 1 && (
        <div style={{ maxWidth: "600px", margin: "auto" }}>
          <div
            style={{ marginTop: "30px" }}
            className="alert alert-success"
            role="alert"
          >
            Database is initialized. use Username: <b>admin</b> to login
          </div>
        </div>
      )}

      <div>
        <form className="login" style={{ backgroundColor: "#f1f1f1" }}>
          <h3 style={{ color: "grey", paddingTop: "10px" }}>
            <FcFlowChart
              style={{
                width: "40px",
                height: "40px",
                marginBottom: "10px",
              }}
            />{" "}
            ERP
          </h3>
          <h3>Login</h3>
          <div className="form-group">
            <label>Username</label>
            <input
              onChange={(e) => setValue({ ...value, username: e.target.value })}
              value={value.username}
              type="username"
              className="form-control"
              placeholder="Enter Username"
            />
          </div>
          <div className="form-group">
            <label>Password</label>
            <input
              onChange={(e) => setValue({ ...value, password: e.target.value })}
              value={value.password}
              type="password"
              className="form-control"
              placeholder="Enter Password"
            />
          </div>
          <div className="form-group"></div>
          <button
            onClick={handleLogin}
            type="submit"
            className="btn btn-primary btn-block"
          >
            Login
          </button>
          {!isLoading && !isError && data.length > 0 && (
            <span className="signup__link">
              Not registered? <Link to={"/registration"}>Create account</Link>
            </span>
          )}
        </form>
        {
          //if database is empty
          !isLoading && !isError && !data.length && (
            <Form
              onSubmit={submit}
              className="login"
              style={{ backgroundColor: "#f1f1f1" }}
            >
              <h3>Initialize Admin</h3>
              <div>
                <div className="form-group">
                  <label>Admin Password</label>
                  <input
                    onChange={(e) => {
                      setPassword(e.target.value)
                    }}
                    type="password"
                    className="form-control"
                    minLength="8"
                    placeholder="Enter Admin Password"
                    required
                  />
                </div>
                <Button
                  type="submit"
                  className="col-12 btn btn-primary btn-block"
                >
                  Initialize Admin
                </Button>
              </div>
            </Form>
          )
        }
      </div>
      <ToastContainer />
    </div>
  )
}

export default Login
