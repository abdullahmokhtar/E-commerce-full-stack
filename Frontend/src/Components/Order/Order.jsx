import React from "react";
import { useQuery } from "react-query";
import { getLoggedUserOrder } from "../../util/http";
import { Helmet } from "react-helmet";
import "./Order.css";

const Order = () => {
  const { data, isLoading } = useQuery({
    queryKey: ["order"],
    queryFn: getLoggedUserOrder,
  });

  return (
    <>
      <Helmet>
        <title>My Orders</title>
      </Helmet>
      {isLoading && (
        <div className="text-center">
          <i className="fas fa-spinner fa-spin fa-2x "></i>
        </div>
      )}

      {!isLoading && data?.length === 0 && (
        <div className="my-3 w-75 m-auto alert alert-secondary text-center">
          <p className=" text-white fw-bold fs-3">
            There is no products in whish List
          </p>
        </div>
      )}

      {!isLoading && (
        <section className="h-100 h-custom">
          <div className="container py-2 ">
            <div className="row d-flex justify-content-center align-items-center ">
              {data?.map((order, index) => (
                <div key={index} className="col-12 my-2">
                  <div
                    className="card border-top  border-3"
                    style={{ borderColor: "#0AAD0A" }}
                  >
                    <div className="card-body px-4 py-3">
                      <p
                        className="lead fw-bold mb-1"
                        style={{ color: "#0AAD0A" }}
                      >
                        Purchase Receipt
                      </p>
                      <div className="row">
                        <div className="col mb-3">
                          <p className="small text-muted fw-bold mb-1">Date</p>
                          <p>
                            {new Date(order?.createdAt).toLocaleDateString()}
                          </p>
                        </div>
                        <div className="col mb-3 ">
                          <p className="small fw-bold text-muted mb-1">
                            Order No.
                          </p>
                          <p>{order?.id}</p>
                        </div>
                      </div>

                      <div
                        className="mx-n5 px-5 py-4"
                        style={{ backgroundColor: "#f2f2f2" }}
                      >
                        <div className="row text-center">
                          <p className="col-md-4 fw-bolder">Product Name</p>
                          <p className="col-md-2 fw-bolder">Quantity</p>
                          <p className="col-md-2 fw-bolder">Price</p>
                          <p className="col-md-4 fw-bolder">Total</p>
                        </div>
                        <hr />
                        {order?.orderItems?.map((orderItem, index) => (
                          <>
                            <div key={index} className="row text-center">
                              <div className="col-md-4">
                                <p className="fw-bolder fst-italic">
                                  {orderItem?.productName}
                                </p>
                              </div>
                              <div className="col-md-2">
                                <p className="d-inline">{orderItem?.quantity}</p>
                              </div>
                              <div className="col-md-2">
                                <p className="d-inline">
                                  EGP{" "}
                                  {new Intl.NumberFormat().format(
                                    orderItem?.price
                                  )}
                                </p>
                              </div>
                              <div className="col-md-4 ">
                                <p className="d-inline">
                                  EGP{" "}
                                  {new Intl.NumberFormat().format(
                                    orderItem?.price * orderItem?.quantity
                                  )}
                                </p>
                              </div>
                            </div>
                            <hr />
                          </>
                        ))}
                      </div>

                      <div className="row my-4">
                        <div className="col-md-8">
                          <p className="fw-bolder my-1">
                            Location: {order?.shippingAddress?.city}
                          </p>
                          <p className="fw-light my-0 text-nowrap overflow-hidden">
                            Details: {order?.shippingAddress?.details}
                          </p>
                        </div>
                        <div className="col-md-4">
                          <p
                            className="lead fw-bold mb-0"
                            style={{ color: "#0AAD0A" }}
                          >
                            EGP{" "}
                            {new Intl.NumberFormat().format(
                              order?.totalOrderPrice
                            )}
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </section>
      )}
    </>
  );
};

export default Order;
