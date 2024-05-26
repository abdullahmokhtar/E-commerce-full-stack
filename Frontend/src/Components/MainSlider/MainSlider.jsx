import Slider from "react-slick";
import slide1 from "../../assets/images/slider-image-3.jpeg";
import slide2 from "../../assets/images/blog-img-2.jpeg";
import img1 from "../../assets/images/1.jpg";
import img2 from "../../assets/images/2.jpg";

const MainSlider = () => {
  const settings = {
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false
  };
  return (
    <div className="container mt-5">
      <div className="row">
        <div className="col-md-9 p-0">
          <Slider {...settings}>
            <img className="w-100" height={500} src={slide1} alt="slide" />
            <img className="w-100" height={500} src={slide2} alt="slide" />
          </Slider>
        </div>
        <div className="col-md-3 p-0">
          <img src={img1} height={250} className="w-100" alt="" />
          <img src={img2} height={250} className="w-100" alt="" />
        </div>
      </div>
    </div>
  );
};

export default MainSlider;
