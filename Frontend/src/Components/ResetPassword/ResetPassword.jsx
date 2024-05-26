import { useFormik } from "formik";
import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { object, string, ref } from "yup";
import { Helmet } from "react-helmet";
import { resetPassword } from "../../util/http";

const ResetPassword = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();

  const updatePassword = async () => {
    setIsLoading(true);
    setErrorMessage("");
    const { status } = await resetPassword(formik.values).catch((err) => {
      setErrorMessage(err.response.data);
      setIsLoading(false);
    });
    setIsLoading(false);
    if (status === 200) {
      navigate("/login", { replace: true });
    }
  };

  const validationSchema = object({
    email: string()
      .required("Email is required")
      .matches(
        /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
        "Invalid email address"
      ),
    password: string()
      .required("Password is required")
      .matches(
        /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8}$/,
        "Password must contain a special character, number and greater than 8 characters and less than 18 characters"
      ),
    rePassword: string()
      .required("RePassword is required")
      .oneOf([ref("password")], "Password and Repassword does not match"),
  });
  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
      rePassword: "",
    },
    validationSchema,
    onSubmit: updatePassword,
  });
  return (
    <>
      <Helmet>
        <title>Reset Password</title>
      </Helmet>
      <div className="w-75 m-auto my-5">
        <h1>Reset Password Now:</h1>
        <form onSubmit={formik.handleSubmit}>
          <label htmlFor="email" className="my-1">
            Email:
          </label>
          <input
            onBlur={formik.handleBlur}
            value={formik.values.email}
            onChange={formik.handleChange}
            type="email"
            name="email"
            id="email"
            className="form-control mb-3"
          />
          {formik.errors.email && formik.touched.email && (
            <div className="alert alert-danger">{formik.errors.email}</div>
          )}

          <label htmlFor="password" className="my-1">
            New Password:
          </label>
          <input
            onBlur={formik.handleBlur}
            value={formik.values.password}
            onChange={formik.handleChange}
            type="password"
            name="password"
            id="password"
            className="form-control mb-3"
          />
          {formik.errors.password && formik.touched.password && (
            <div className="alert alert-danger">{formik.errors.password}</div>
          )}

          <label htmlFor="rePassword" className="my-1">
            RePassword:
          </label>
          <input
            onBlur={formik.handleBlur}
            value={formik.values.rePassword}
            onChange={formik.handleChange}
            type="password"
            name="rePassword"
            id="rePassword"
            className="form-control mb-3"
          />
          {formik.errors.rePassword && formik.touched.rePassword && (
            <div className="alert alert-danger">{formik.errors.rePassword}</div>
          )}

          {errorMessage && (
            <div className="alert alert-danger">{errorMessage}</div>
          )}

          {!isLoading ? (
            <button
              disabled={!(formik.isValid && formik.dirty)}
              type="submit"
              className="btn bg-main text-white px-3 ms-auto d-block"
            >
              Reset Password
            </button>
          ) : (
            <button
              disabled
              type="button"
              className="btn bg-main text-white px-3 ms-auto d-block"
            >
              <i className="fas fa-spin fa-spinner"> </i>
            </button>
          )}
        </form>
      </div>
    </>
  );
};

export default ResetPassword;
