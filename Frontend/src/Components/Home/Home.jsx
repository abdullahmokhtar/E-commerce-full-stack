import Product from "../Product/Product";
import MainSlider from "../MainSlider/MainSlider";
import CategorySlider from "../CategorySlider/CategorySlider";
import { useQuery } from "react-query";
import { Helmet } from "react-helmet";
import { getLoggedUserWishList, getProducts } from "../../util/http";
import { useState } from "react";
import Pagination from "../Pagination/Pagination";

const Home = () => {
  const [page, setPage] = useState(1);
  const { data, isError, isLoading } = useQuery({
    queryKey: ["products", page],
    queryFn: () => getProducts(page, 0),
    keepPreviousData: true,
    cacheTime: 1000
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
        {data?.data?.map((product) => {
          return (
            <Product
              fav={wishlist?.filter((pro) => pro.id === product.id)}
              key={product.id}
              product={product}
            />
          );
        })}
        <Pagination
          decrementPage={() => setPage((prev) => prev - 1)}
          incrementPage={() => setPage((prev) => prev + 1)}
          pageNumber={data?.pageNumber}
          totalPages={data?.totalPages}
        />
      </div>
    </>
  );
};

export default Home;
