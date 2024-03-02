import React from "react";
import DestinationSlider from "./Helper/DestinationSlider";

const TopDestination = () => {
  return (
    <div className="mt-[6rem] mb-[4rem] ">
      <h1 className="heading">Top Destination</h1>
      <div className="mt-[4rem] w-[80%] mx-auto ">
        <DestinationSlider/>
      </div>
    </div>
  );
};

export default TopDestination;
