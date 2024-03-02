import React from "react";
import HotelCard from "./Helper/HotelCard";

const Hotel = () => {
  return (
    <div className="pt-[5rem] bg-gray-200 pb-[4rem]">
      <h1 className="heading">Best Hotel</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-[3rem] items-center w-[80%] mx-auto mt-[4rem]">
        <div>
          <HotelCard
            name="Hotel A"
            city="Rajishahi"
            price="$123"
            reviewNum="21"
            image="/images/h1.png"
          />
        </div>
        <div>
          <HotelCard
            name="Hotel B"
            city="Dhaka"
            price="$223"
            reviewNum="31"
            image="/images/h2.png"
          />
        </div>
        <div>
          <HotelCard
            name="Hotel C"
            city="Japan"
            price="$423"
            reviewNum="51"
            image="/images/h3.png"
          />
        </div>
        <div>
          <HotelCard
            name="Hotel D"
            city="Indo"
            price="$623"
            reviewNum="61"
            image="/images/h4.png"
          />
        </div>
        <div>
          <HotelCard
            name="Hotel E"
            city="Osaka"
            price="$123"
            reviewNum="51"
            image="/images/h5.png"
          />
        </div>
        <div>
          <HotelCard
            name="Hotel F"
            city="Jakarta"
            price="$323"
            reviewNum="41"
            image="/images/h6.png"
          />
        </div>
      </div>
    </div>
  );
};

export default Hotel;
