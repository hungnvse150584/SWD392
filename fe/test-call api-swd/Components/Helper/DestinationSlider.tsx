import React from "react";
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";

const responsive = {
  desktop: {
    breakpoint: { max: 3000, min: 1300 },
    items: 5,
    slidesToSlide:1
  },
  tablet: {
    breakpoint: { max: 1300, min: 764 },
    items: 3,
    slidesToSlide:1
  },
  mobile: {
    breakpoint: { max: 764, min: 0 },
    items: 1,
    slidesToSlide:1
  },
};

const DestinationSlider = () => {
  return (
    <Carousel
      additionalTransfrom={0}
      arrows={true}
      autoPlay={true}
      autoPlaySpeed={5000}
      centerMode={false}
      infinite
      responsive={responsive}
      itemClass="item"
    >
      <div>
        <img
          src="/images/d1.png"
          alt="destination"
          width={200}
          height={200}
          className="rounded-full mx-auto"
        />
        <h1 className="destination__h1">New York</h1>
        <p className="destination__p">7 Hotel</p>
      </div>
      <div>
        <img
          src="/images/d2.png"
          alt="destination"
          width={200}
          height={200}
          className="rounded-full mx-auto"
        />
        <h1 className="destination__h1">Japan</h1>
        <p className="destination__p">17 Hotel</p>
      </div>
      <div>
        <img
          src="/images/d3.png"
          alt="destination"
          width={200}
          height={200}
          className="rounded-full mx-auto"
        />
        <h1 className="destination__h1">Banglades</h1>
        <p className="destination__p">3 Hotel</p>
      </div>
      <div>
        <img
          src="/images/d4.png"
          alt="destination"
          width={200}
          height={200}
          className="rounded-full mx-auto"
        />
        <h1 className="destination__h1">India</h1>
        <p className="destination__p">27 Hotel</p>
      </div>
      <div>
        <img
          src="/images/d5.png"
          alt="destination"
          width={200}
          height={200}
          className="rounded-full mx-auto"
        />
        <h1 className="destination__h1">England</h1>
        <p className="destination__p">37 Hotel</p>
      </div>
      <div>
        <img
          src="/images/d6.png"
          alt="destination"
          width={200}
          height={200}
          className="rounded-full mx-auto"
        />
        <h1 className="destination__h1">Australia</h1>
        <p className="destination__p">23 Hotel</p>
      </div>
    </Carousel>
  );
};

export default DestinationSlider;
