import { useFormik } from "formik";
import React, { useState } from "react";
import { Helmet } from "react-helmet";
import { Link } from "react-router-dom";
import { object, string } from "yup";
import { getLoggedUserData, updateUserData } from "../../util/http";
import { useQuery } from "react-query";
import toast from "react-hot-toast";

const UserData = () => {
  const [errorMessage, setErrorMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const edit = async () => {
    setIsLoading(true);
    setErrorMessage("");
    const { status } = await updateUserData(formik.values).catch((err) => {
      setErrorMessage(err.response.data);
      setIsLoading(false);
    });
    setIsLoading(false);
    if (status === 200) {
      toast.success("User data updated successfully");
    }
  };

  const validationSchema = object({
    userName: string()
      .required("Name is required")
      .min(3, "minimum length must be at least 3")
      .max(20, "maximum length must be at least 20"),
    email: string()
      .required("Email is required")
      .matches(
        /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
        "Invalid email address"
      ),
    phoneNumber: string()
      .required("Phone is required")
      .matches(/^01[0125][0-9]{8}$/, "Invalid phone number"),
  });

  const { data, isLoading: isLoadingUserData } = useQuery({
    queryKey: ["userData"],
    queryFn: getLoggedUserData,
  });

  const formik = useFormik({
    initialValues: {
      userName: data?.userName,
      email: data?.email,
      phoneNumber: data?.phoneNumber,
    },
    enableReinitialize: true,
    validationSchema,
    onSubmit: edit,
  });

  return (
    <>
      <Helmet>
        <title>User Data</title>
      </Helmet>
      <div className="w-75 m-auto my-5">
        <h1>{data?.userName?.toUpperCase() + "'s Data:"}</h1>
        {isLoading && (
          <div className="text-center">
            <i className="fas fa-spinner fa-spin fa-2x "></i>
          </div>
        )}
        {!isLoadingUserData && (
          <form onSubmit={formik.handleSubmit}>
            <label htmlFor="userName" className="my-1">
              Name:
            </label>
            <input
              type="text"
              name="userName"
              id="userName"
              className="form-control mb-3"
              onChange={formik.handleChange}
              value={formik.values.userName}
              onBlur={formik.handleBlur}
            />
            {formik.errors.userName && formik.touched.userName && (
              <div className="alert alert-danger">{formik.errors.userName}</div>
            )}

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

            <label htmlFor="phoneNumber" className="my-1">
              Phone:
            </label>
            <input
              onBlur={formik.handleBlur}
              value={formik.values.phoneNumber}
              onChange={formik.handleChange}
              type="tel"
              name="phoneNumber"
              id="phoneNumber"
              maxLength="11"
              className="form-control mb-3"
            />
            {formik.errors.phoneNumber && formik.touched.phoneNumber && (
              <div className="alert alert-danger">
                {formik.errors.phoneNumber}
              </div>
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
                Edit
              </button>
            ) : (
              <button
                disabled
                type="button"
                className="btn bg-main text-white px-3 ms-auto d-block"
              >
                <i className="fas fa-spin fa-spinner "> </i>
              </button>
            )}
          </form>
        )}
        <Link to="/change-pass" className="btn btn-info text-white fw-bold">
          Change Password
        </Link>
      </div>
    </>
  );
};

export default UserData;
