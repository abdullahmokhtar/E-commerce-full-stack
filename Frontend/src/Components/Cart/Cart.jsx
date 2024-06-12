import { Helmet } from "react-helmet";
import { useMutation, useQuery } from "react-query";
import { Link } from "react-router-dom";
import {
  deleteProduct,
  getLoggedUserCart,
  queryClient,
  updateProduct,
} from "../../util/http";

let timerId;

const Cart = () => {

const { data, isError, isLoading } = useQuery({
  queryKey: ["cart"],
  queryFn: getLoggedUserCart,
});

console.log(data);

  const { mutate, isLoading: isLoadingDeletion } = useMutation({
    mutationFn: deleteProduct,
    onSuccess: () => {
      queryClient.resetQueries({ queryKey: ["cart"] });
    },
  });

  const { mutate: updateProductCount } = useMutation({
    onMutate: async (data) => {
      await queryClient.cancelQueries(["cart"]);
      const prevCart = queryClient.getQueryData(["cart"]);
      if (data.count <= 0) {
        mutate({ id: prevCart.cartProducts[data.index].product.id });
        return;
      }
      if (data.count > 20) return;
      const newCart = { ...prevCart };
      newCart.cartProducts[data.index].quantity = data.count;
      newCart.totalCartPrice = prevCart.cartProducts.reduce(
        (acc, curr) =>
          acc.product?.price * acc.quantity + curr.product?.price * curr.quantity
      );
      // await queryClient.setQueryData(["cart"], newCart);
      clearTimeout(timerId);
      timerId = setTimeout(() => {
        updateProduct({ id: data.productId, count: data.count });
      }, 500);
      return { prevCart };
    },
  });

  
  return (
    <>
      <Helmet>
        <title>Cart</title>
      </Helmet>
      {(isLoading || isLoadingDeletion) && (
        <div className="text-center">
          <i className="fas fa-spinner fa-spin fa-2x py-5 my-5"></i>
        </div>
      )}
      {isError && !isLoading && !isLoadingDeletion && (
        <h1 className="alert alert-warning text-center my-5">
          No Products found in your cart list add more products
        </h1>
      )}
      {!isLoading && !isLoadingDeletion && data?.cartProducts?.length > 0 && (
        <div className="my-5">
          <button
            className="btn btn-outline-danger d-block ms-auto"
            onClick={() => {
              mutate({});
            }}
          >
            Clear Cart
          </button>
          {data?.cartProducts?.map((product, index) => (
            <div key={index} className="cart-product shadow rounded-2 my-3">
              <div className="row align-items-center">
                <div className="col-md-2">
                  <img
                    className="w-100"
                    src={product.product.imageCover}
                    alt={product.product.name}
                  />
                </div>
                <div className="col-md-8">
                  <h2>{product.product.name}</h2>
                  <h5>{product.product?.category?.name}</h5>
                  <p className="d-flex justify-content-between">
                    <span>{product?.product?.price} EGP</span>
                    <span>
                      <i className="fas fa-star rating-color me-1"></i>{" "}
                      {product?.product?.ratingsAverage}
                    </span>
                  </p>
                  <p>
                    <span className="fw-bolder">Total Price:</span>{" "}
                    {product?.quantity * product?.product?.price} EGP
                  </p>
                </div>
                <div className="col-md-2">
                  <button
                    className="btn bg-danger text-white mb-2 mx-2"
                    onClick={() => {
                      mutate({ id: product?.product?.id });
                    }}
                  >
                    Remove
                  </button>
                  <div className="d-flex align-items-center">
                    <button
                      onClick={() => {
                        updateProductCount({
                          index,
                          count: product.quantity - 1,
                          productId: product?.product?.id,
                        });
                      }}
                      className="btn bg-main text-white mx-2"
                    >
                      -
                    </button>
                    <span>{product.quantity}</span>
                    <button
                      onClick={() => {
                        updateProductCount({
                          index,
                          count: product.quantity + 1,
                          productId: product.product.id,
                        });
                      }}
                      className="btn bg-main text-white mx-2"
                    >
                      +
                    </button>
                  </div>
                </div>
              </div>
            </div>
          ))}
          {data?.totalCartPrice && (
            <div className="d-flex justify-content-between">
              <Link to={`/address`} className="btn bg-main text-white">
                CheckOut
              </Link>
              {/* <p>Total Cart Price: {data?.totalCartPrice} EGP</p> */}
              <p>
                Total Cart Price:{" "}
                {isNaN(
                  data?.cartProducts?.reduce(
                    (c, a) =>
                      c?.quantity * c?.product?.price +
                      a?.quantity * a?.product?.price
                  )
                )
                  ? data?.totalCartPrice
                  : data?.cartProducts?.reduce(
                      (c, a) =>
                        c?.quantity * c?.product?.price +
                        a?.quantity * a?.product?.price
                    )}{" "}
                EGP
              </p>
            </div>
          )}
        </div>
      )}
    </>
  );
};

export default Cart;
