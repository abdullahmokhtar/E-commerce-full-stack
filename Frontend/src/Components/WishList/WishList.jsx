import { Helmet } from "react-helmet";
import {
  addProdToCart,
  deleteProductFromWishList,
  getLoggedUserWishList,
  queryClient,
} from "../../util/http";
import { useMutation, useQuery } from "react-query";
import toast from "react-hot-toast";

const WishList = () => {

  const { data, isLoading } = useQuery({
    queryKey: ["wishlist"],
    queryFn: getLoggedUserWishList,
  });

  const { mutate: deleteProduct, isLoading: isLoadingDeletion } = useMutation({
    mutationFn: deleteProductFromWishList,
    onSuccess: () => {
      queryClient.resetQueries({ queryKey: ["wishlist"] });
    },
  });

  const addProductToCart = async (productId) => {
    const response = await addProdToCart({ productId }).catch((err) => {
      toast.error(err.response.data);
    });
    if (response) {
      toast.success(response.data);
    }
  };
  return (
    <>
      <Helmet>
        <title>Wish List</title>
      </Helmet>

      {(isLoading || isLoadingDeletion) && (
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

      {!isLoading &&
        data?.map((product) => (
          <div key={product.id} className="cart-product shadow rounded-2 my-3">
            <div className="row align-items-center">
              <div className="col-md-2">
                <img
                  className="w-100"
                  src={product.imageCover}
                  alt={product.title}
                />
              </div>
              <div className="col-md-7">
                <h2>{product?.name}</h2>
                <h5 className="text-info">{product.brand.name}</h5>
                <h5>{product.category.name}</h5>
                <p className="d-flex justify-content-between">
                  <span className="text-main">{product.price} EGP</span>
                  <span>
                    <i className="fas fa-star rating-color me-1"></i>
                    {product.ratingsAverage}
                  </span>
                </p>
              </div>
              <div className="col-md-3">
                <button
                  onClick={() => {
                    deleteProduct({ id: product.id });
                  }}
                  className="btn bg-danger text-white w-75 mb-2 mx-2"
                >
                  Remove
                </button>
                <button
                  onClick={() => {
                    addProductToCart(product.id);
                  }}
                  className="btn bg-success text-white w-75 mb-2 mx-2"
                >
                  Add To Cart
                </button>
              </div>
            </div>
          </div>
        ))}
    </>
  );
};

export default WishList;
