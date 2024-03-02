import Contact from "@/Components/Contact";
import Footer from "@/Components/Footer";
import Hero from "@/Components/Hero";
import Hotel from "@/Components/Hotel";
import MobileNav from "@/Components/MobileNav";
import Navbar from "@/Components/Navbar";
import Reviews from "@/Components/Reviews";
import TopDestination from "@/Components/TopDestination";
import React, { useState } from "react";

const Homepage = () => {
  const [nav, setNav] = useState(false);
  const openNavHandler = () => setNav(true);
  const closeNavHandler = () => setNav(false);

  return (
    <div className="overflow-x-hidden">
      <MobileNav nav={nav} closeNav={closeNavHandler} />
      <Navbar openNav={openNavHandler} />
      <Hero/>
      <TopDestination/>
      <Hotel/>
      <Reviews/>
      <Contact/>
      <Footer/>
    </div>
  );
};

export default Homepage;
