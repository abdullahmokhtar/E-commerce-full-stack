import { useFormik } from "formik";
import React, { useState } from "react";
import { Helmet } from "react-helmet";
import { submitOrder } from "../../util/http";
import { string, object } from "yup";

const Address = () => {
  const [error, serError] = useState("");

  const validationSchema = object({
    details: string()
      .required("Details is required")
      .min(5, "minimum length must be at least 5")
      .max(255, "maximum length must be at least 255"),
    city: string()
      .required("City is required")
      .min(3, "minimum length must be at least 3")
      .max(50, "maximum length must be at least 50"),
    phone: string()
      .required("Phone is required")
      .matches(/^01[0125][0-9]{8}$/, "Invalid phone number"),
  });

  const formik = useFormik({
    initialValues: {
      details: "",
      phone: "",
      city: "",
    },
    onSubmit: (values) => {
      submitOrder(values).catch((err) => {
        serError(err.message);
      });
    },
    validationSchema,
  });
  return (
    <>
      <Helmet>
        <title>Address</title>
      </Helmet>
      {error !== "" && (
        <div className="alert text-center fs-5 fw-bold my-2 alert-danger">
          {error}
        </div>
      )}
      <form className="w-75 m-auto my-3" onSubmit={formik.handleSubmit}>
        <label htmlFor="details" className="my-1">
          Details:
        </label>
        <input
          value={formik.values.details}
          onChange={formik.handleChange}
          name="details"
          id="details"
          type="text"
          className="form-control mb-3"
        />
        {formik.errors.details && formik.touched.details && (
          <div className="alert alert-danger">{formik.errors.details}</div>
        )}
        <label htmlFor="phone" className="my-1">
          Phone:
        </label>
        <input
          onBlur={formik.handleBlur}
          value={formik.values.phone}
          onChange={formik.handleChange}
          name="phone"
          id="phone"
          type="tel"
          className="form-control mb-3"
        />
        {formik.errors.phone && formik.touched.phone && (
          <div className="alert alert-danger">{formik.errors.phone}</div>
        )}
        <label htmlFor="city" className="my-1">
          City:
        </label>
        <input
          onBlur={formik.handleBlur}
          value={formik.values.city}
          onChange={formik.handleChange}
          name="city"
          id="city"
          type="text"
          className="form-control mb-3"
        />
        {formik.errors.city && formik.touched.city && (
          <div className="alert alert-danger">{formik.errors.city}</div>
        )}
        <button
          type="submit"
          className="btn bg-main text-white px-3 ms-auto d-block"
        >
          Order
        </button>
      </form>
    </>
  );
};

export default Address;
