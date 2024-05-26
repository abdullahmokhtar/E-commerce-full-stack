import { useState } from "react";
import toast from "react-hot-toast";
import { Link } from "react-router-dom";
import {
  queryClient,
  deleteProductFromWishList,
  addProdToWishlist,
  addProdToCart,
} from "../../util/http";

function Product({ product, fav }) {
  const [isFav, setIsFav] = useState(false);

  const addProductToWishlist = async () => {
    const response = await addProdToWishlist({ productId: product.id }).catch(
      (err) => {
        if(err.response.status === 401)
          toast.error("You must login before adding product to wishlist");
        else
          toast.error(err.response.data);
      }
    );
    if (response) {
      setIsFav(true);
      toast.success(response.data);
      queryClient.invalidateQueries({ queryKey: ["products"] });
    }
  };

  // const { mutate: deleteProduct, isLoading: isLoadingDeletion } = useMutation({
  //   mutationFn: deleteProductFromWishList,
  //   onSuccess: () => {
  //     queryClient.resetQueries({ queryKey: ["wishlist"] });
  //   },
  // });

  const removeProductFromWishlist = async () => {
    let data = await deleteProductFromWishList({ id: product.id });
    if (data) {
      setIsFav(false);
      toast.success(data);
      queryClient.resetQueries({ queryKey: ["wishlist"] });
    }
  };

  const addProductToCart = async () => {
    const response = await addProdToCart({ productId: product.id }).catch(
      (err) => {
          if (err.response.status === 401)
            toast.error("You must login before adding product to cart");
          else toast.error(err.response.data);
      }
    );
    if (response) {
      toast.success(response.data);
    }
  };
  return (
    <div className="col-md-3" role="button">
      <div className="product px-2 py-3 overflow-hidden">
        <Link
          to={`/ProductDetails/${product.id}`}
          className="text-decoration-none"
        >
          <img className="w-100" src={product.imageCover} alt={product.name} />
          <div className="d-flex justify-content-between px-1">
            <h5 className="font-sm text-main">{product.category.name}</h5>
            <h5 className="font-sm text-info">{product.brand.name}</h5>
          </div>
          <h4 className="fs-5">
            {product.name.split(" ").slice(0, 2).join(" ")}
          </h4>
          <p className="d-flex justify-content-between">
            <span className="ms-1">{product.price} EGP</span>
            <span>
              {product.ratingsAverage}
              <i className="fas fa-star rating-color me-1 "></i>
            </span>
          </p>
        </Link>
        <div className="d-flex justify-content-between align-items-center">
          <button
            onClick={addProductToCart}
            className="btn bg-main text-white w-75"
          >
            + Add To Cart
          </button>
          {fav?.length > 0 || isFav ? (
            <i
              onClick={removeProductFromWishlist}
              style={{ color: "red" }}
              className="fa-solid fa-heart h3"
            ></i>
          ) : (
            <i
              onClick={addProductToWishlist}
              style={{ color: "black" }}
              className="fa-solid fa-heart h3"
            ></i>
          )}
        </div>
      </div>
    </div>
  );
}

export default Product;
