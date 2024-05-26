import { Helmet } from "react-helmet";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import Slider from "react-slick";
import {
  addProdToCart,
  addProdToWishlist,
  deleteProductFromWishList,
  getLoggedUserWishList,
  getProducts,
  queryClient,
} from "../../util/http";
import toast from "react-hot-toast";
import { useState } from "react";

const ProductDetails = () => {
  const { id } = useParams();

  const { data: wishlist, isLoading: isWishListLoading } = useQuery({
    queryFn: getLoggedUserWishList,
    queryKey: ["wishlist"],
  });

  const fav = wishlist?.filter((wishlist) => wishlist.id === +id).length > 0;

  const [isFav, setIsFav] = useState(false);

  const addProductToCart = async () => {
    const response = await addProdToCart({ productId: id }).catch((err) => {
      toast.error(err.response.data);
    });
    if (response) {
      toast.success(response.data);
    }
  };

  const removeProductFromWishlist = async () => {
    const data = await deleteProductFromWishList({ id });
    if (data) {
      setIsFav(false);
      toast.success(data);
      queryClient.resetQueries({ queryKey: ["wishlist"] });
    }
  };

  const addProductToWishlist = async () => {
    const response = await addProdToWishlist({ productId: id }).catch((err) => {
      toast.error(err.response.data);
    });
    if (response) {
      setIsFav(true);
      toast.success(response.data);
      queryClient.invalidateQueries({ queryKey: ["wishlist"] });
    }
  };

  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
  };

  const { data, isError, isLoading } = useQuery({
    queryKey: ["productDetails", id],
    queryFn: () => getProducts({ id }),
  });

  return (
    <>
      <Helmet>
        <title>{data?.title}</title>
      </Helmet>
      {isError && (
        <div className="alert alert-warning text-center my-5">
          <p className="text-danger">
            An error occurred while fetching the products Details
          </p>
        </div>
      )}
      {!isLoading && !isError && (
        <div className="container">
          <div className="row align-items-center py-5">
            <div className="col-md-4">
              <Slider {...settings}>
                {data?.images?.map((img, index) => (
                  <img
                    loading="lazy"
                    key={index}
                    className="w-100"
                    src={img}
                    alt={data?.title}
                  />
                ))}
              </Slider>
            </div>
            <div className="col-md-8">
              <h2 className="mb-1 fw-bold">{data?.name}</h2>
              <div className="d-flex justify-content-between">
                <h5 className="font-sm text-main mt-2">
                  {data?.category?.name}
                </h5>
                <h5 className="font-sm text-info mt-2">
                  {data?.brand?.name?.toUpperCase()}
                </h5>
              </div>
              <p className="mt-2">{data?.description}</p>
              <p className="d-flex justify-content-between mt-2">
                <span>{data?.price} EGP</span>
                <span>
                  <i className="fas fa-star rating-color me-1"></i>
                  <span> {data?.ratingsAverage}</span>
                </span>
              </p>
              <div className="d-flex justify-content-between align-items-center mt-4">
                <button
                  onClick={addProductToCart}
                  className="btn bg-main text-white w-75 mx-auto"
                >
                  + Add To Cart
                </button>
                {!isWishListLoading &&
                  (fav || isFav ? (
                    <i
                      onClick={removeProductFromWishlist}
                      style={{ color: "red" }}
                      className="fa-solid fa-heart h3"
                      role="button"
                    ></i>
                  ) : (
                    <i
                      onClick={addProductToWishlist}
                      style={{ color: "black" }}
                      className="fa-solid fa-heart h3"
                      role="button"
                    ></i>
                  ))}
                {/* {isFav ? (
                  <i
                    onClick={removeProductFromWishlist}
                    style={{ color: "red" }}
                    className="fa-solid fa-heart h3"
                    role="button"
                  ></i>
                ) : (
                  <i
                    onClick={addProductToWishlist}
                    style={{ color: "black" }}
                    className="fa-solid fa-heart h3"
                    role="button"
                  ></i>
                )} */}
              </div>
            </div>
          </div>
        </div>
      )}
      {isLoading && (
        <div className="d-flex align-items-center my-5 py-5 justify-content-center">
          <i className="fas fa-spin fa-spinner fa-2x"></i>
        </div>
      )}
    </>
  );
};

export default ProductDetails;
