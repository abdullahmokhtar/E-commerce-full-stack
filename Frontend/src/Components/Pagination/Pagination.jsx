import React from "react";

const Pagination = ({
  pageNumber,
  decrementPage,
  incrementPage,
  totalPages,
}) => {
  return (
    <nav className="mt-3 py-2 mb-2">
      <ul className="m-auto pagination p-0">
        <li className={`page-item ${pageNumber === 1 ? "disabled" : ""}`}>
          <button className="page-link" onClick={decrementPage}>
            Previous
          </button>
        </li>
        {pageNumber > 1 && (
          <li className="page-item">
            <button className="page-link" onClick={decrementPage}>
              {pageNumber - 1}
            </button>
          </li>
        )}
        <li className="page-item active">
          <button className="page-link">
            {pageNumber}
            <span className="sr-only">(current)</span>
          </button>
        </li>
        {pageNumber !== totalPages && (
          <li className="page-item">
            <button className="page-link" onClick={incrementPage}>
              {pageNumber + 1}
            </button>
          </li>
        )}
        <li
          className={`page-item ${pageNumber === totalPages ? "disabled" : ""}`}
        >
          <button className="page-link" onClick={incrementPage}>
            Next
          </button>
        </li>
      </ul>
    </nav>
  );
};

export default Pagination;
