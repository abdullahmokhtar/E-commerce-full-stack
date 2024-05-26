import { useQuery } from "react-query";
import Slider from "react-slick";
import { getCategories } from "../../util/http";
import React from "react";

const CategorySlider = () => {
  const { data } = useQuery({
    queryKey: ["categories"],
    queryFn: getCategories,
  });

  const settings = {
    infinite: true,
    speed: 500,
    slidesToShow: 6,
    slidesToScroll: 1,
    arrows: false,
    autoplay: true,
    responsive: [
      {
        breakpoint: 992,
        settings: {
          slidesToShow: 4,
          slidesToScroll: 4,
          infinite: true,
          dots: true,
        },
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
        },
      },
      {
        breakpoint: 576,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
        },
      },
    ],
  };
  return (
    <>
      <h3 className="fw-bold my-2">Shop Popular Categories</h3>
      <Slider  {...settings} className="mb-4">
        {data?.map((category, index) => (
          <React.Fragment key={index}>
            <img
              className="w-100 px-1"
              height={250}
              src={category.image}
              alt={category.name}
            />
            <h5 className="font-sm text-main">{category?.name}</h5>
          </React.Fragment>
        ))}
      </Slider>
    </>
  );
};

export default CategorySlider;
