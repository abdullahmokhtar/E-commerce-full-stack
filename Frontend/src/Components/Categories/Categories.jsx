import { Helmet } from "react-helmet";
import { useQuery } from "react-query";
import { getCategories, getSubCategories } from "../../util/http";
import { useState } from "react";
import Pagination from "../Pagination/Pagination";

const Categories = () => {
  const [subCategories, setSubCategories] = useState([]);
  const [categoryName, setCategoryName] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [page, setPage] = useState(1);
  const { data } = useQuery({
    queryKey: ["categories", page],
    queryFn: () => getCategories(page),
  });

  const getSubCategory = async (id, name) => {
    setIsLoading(true);
    setCategoryName(name);
    setSubCategories([]);
    const data = await getSubCategories(id);
    setIsLoading(false);
    if (data.length > 0) {
      setSubCategories(data);
    }
  };
  return (
    <>
      <Helmet>
        <title>Categories</title>
      </Helmet>
      <div className="container my-5 py-5">
        <div className="row g-4">
          {data?.data?.map((category) => (
            <div key={category.id} className="col-md-4">
              <div
                onClick={() => getSubCategory(category.id, category.name)}
                className="card"
                role="button"
              >
                <div className="card-img">
                  <img
                    height={300}
                    className="w-100"
                    src={category.image}
                    alt={category.name}
                  />
                </div>
                <div className="card-body">
                  <p className="text-success h3 text-center">{category.name}</p>
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
      </div>
      {isLoading && (
        <div className="text-center">
          <i className="fas fa-spinner fa-spin fa-2x py-5 my-5"></i>
        </div>
      )}
      {!isLoading && (
        <div className="container mb-3">
          {categoryName && (
            <h2 className="text-center text-main my-4 fs-2 fw-bold">
              {categoryName} subcategories
            </h2>
          )}
          {subCategories && (
            <div className="row gy-3">
              {subCategories.map((subCategory, index) => (
                <div key={index} className="col-md-4">
                  <div className="card px-2 py-3">
                    <p className="h3 text-center fs-5  fw-bold">
                      {subCategory.name}
                    </p>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      )}
    </>
  );
};

export default Categories;
