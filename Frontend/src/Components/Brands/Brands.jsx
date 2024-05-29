import { useQuery } from "react-query";
import { getBrands } from "../../util/http";
import { Helmet } from "react-helmet";
import { useState } from "react";
import Pagination from "../Pagination/Pagination";

const Brands = () => {
  const [error] = useState("");
  const [page, setPage] = useState(1);

  const { data, isLoading, status } = useQuery({
    queryKey: ["brands", page],
    queryFn: () => getBrands(page),
    keepPreviousData: true,
  });
  return (
    <>
      <Helmet>
        <title>Brands</title>
      </Helmet>
      <h1 className="text-main text-center fw-bolder my-4">All Brands</h1>
      <div className="container mb-5">
        {error !== "" && (
          <div className="alert alert-warning text-center">
            <p className="text-danger fs-3 my-3 fw-bold">{error}</p>
          </div>
        )}
        {isLoading && (
          <div className="text-center">
            <i className="fas fa-spinner fa-spin fa-2x py-5 my-5"></i>
          </div>
        )}
        {status === "success" && (
          <div className="row g-4">
            {data?.data?.map((brand) => (
              <div key={brand?.id} className="col-md-3">
                <div className="card">
                  <div className="card-img">
                    <img
                      className="w-100"
                      src={brand?.image}
                      alt={brand?.name}
                    />
                  </div>
                  <div className="card-body">
                    <p className="text-center">{brand?.name}</p>
                  </div>
                </div>
              </div>
            ))}
            <Pagination
              decrementPage={() => setPage((prev) => prev - 1)}
              incrementPage={() => setPage((prev) => prev + 1)}
              pageNumber={data?.pageNumber}
              totalPages={data?.totalPages}
            />
          </div>
        )}
      </div>
    </>
  );
};

export default Brands;
