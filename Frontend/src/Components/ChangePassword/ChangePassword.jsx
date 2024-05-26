import { useFormik } from "formik";
import React, { useState } from "react";
import { Helmet } from "react-helmet";
import { useNavigate } from "react-router-dom";
import { object, string, ref } from "yup";
import { changePassword } from "../../util/http";

const ChangePassword = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();

  const changePass = async () => {
    setIsLoading(true);
    setErrorMessage("");
    const response = await changePassword(formik.values).catch((err) => {
      setErrorMessage(err.response.data);
      setIsLoading(false);
    });
    setIsLoading(false);
    if (response.status === 200) {
      navigate("/login", { replace: true });
    }
  };

  const validationSchema = object({
    currentPassword: string()
      .required("currentPassword is required")
      .matches(
        /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/,
        "Password must contain a special character, number and greater than 8 characters and less than 16 characters"
      ),
    password: string()
      .required("Password is required")
      .matches(
        /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/,
        "Password must contain a special character, number and greater than 8 characters and less than 16 characters"
      ),
    rePassword: string()
      .required("RePassword is required")
      .oneOf([ref("password")], "Password and Repassword does not match"),
  });

  const formik = useFormik({
    initialValues: {
      currentPassword: "",
      password: "",
      rePassword: "",
    },
    validationSchema,
    onSubmit: changePass,
  });
  return (
    <>
      <Helmet>
        <title>Change Password</title>
      </Helmet>
      <div className="w-75 m-auto my-5">
        <h1>Change Password:</h1>
        <form onSubmit={formik.handleSubmit}>
          <label htmlFor="currentPassword" className="my-1">
            Current Password:
          </label>
          <input
            onBlur={formik.handleBlur}
            value={formik.values.currentPassword}
            onChange={formik.handleChange}
            type="password"
            name="currentPassword"
            id="currentPassword"
            className="form-control mb-3"
          />
          {formik.errors.currentPassword && formik.touched.currentPassword && (
            <div className="alert alert-danger">
              {formik.errors.currentPassword}
            </div>
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
              Change Password
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

export default ChangePassword;
