import Product from "../Product/Product";
import MainSlider from "../MainSlider/MainSlider";
import CategorySlider from "../CategorySlider/CategorySlider";
import { useQuery } from "react-query";
import { Helmet } from "react-helmet";
import { getLoggedUserWishList, getProducts } from "../../util/http";

const Home = () => {
  const { data, isError, isLoading } = useQuery({
    queryKey: ["products"],
    queryFn: getProducts,
  });

    const { data: wishlist } = useQuery({
      queryFn: getLoggedUserWishList,
      queryKey: ["wishlist"],
    });

  return (
    <>
      <Helmet>
        <title>Home</title>
      </Helmet>
      <MainSlider />
      <CategorySlider />
      {isError && (
        <div className="alert alert-warning text-center">
          <p className="text-danger">
            An error occurred while fetching the products
          </p>
        </div>
      )}
      {isLoading && (
        <div className="text-center">
          <i className="fas fa-spinner fa-spin fa-2x "></i>
        </div>
      )}
      <div className="row">
        {data?.map((product) => {
          return <Product fav={wishlist?.filter(pro => pro.id === product.id)} key={product.id} product={product} />;
        })}
      </div>
    </>
  );
};

export default Home;
