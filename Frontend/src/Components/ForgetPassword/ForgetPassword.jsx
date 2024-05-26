import { useFormik } from "formik";
import React from "react";
import { useState } from "react";
import { Helmet } from "react-helmet";
import { useNavigate } from "react-router-dom";
import { object, string } from "yup";
import { forgetPassword } from "../../util/http";

const ForgetPassword = () => {
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const validationSchema = object({
    email: string()
      .required("Email is required")
      .matches(
        /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
        "Invalid email address"
      ),
  });

  const forgetPass = async () => {
    setIsLoading(true);
    const response = await forgetPassword(formik.values);
    setIsLoading(false);
    if (response.status === 200) {
      navigate("/verify-code");
    }
  };

  const formik = useFormik({
    initialValues: {
      email: "",
    },
    validationSchema,
    onSubmit: forgetPass,
  });
  return (
    <>
      <Helmet>
        <title>Forget Password</title>
      </Helmet>
      <div className="w-75 m-auto my-5">
        <h1>Resting Password:</h1>
        <form onSubmit={formik.handleSubmit}>
          <label htmlFor="email" className="my-1">
            Email:
          </label>

          <input
            value={formik.values.email}
            onChange={formik.handleChange}
            type="email"
            name="email"
            id="email"
            className="form-control mb-3"
          />
          {!isLoading ? (
            <button
              disabled={!formik.isValid}
              type="submit"
              className="btn bg-main text-white px-3 ms-auto d-block"
            >
              Verify
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

export default ForgetPassword;
