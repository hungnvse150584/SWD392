import React from "react";
import { FaFacebookF, FaInstagram, FaTwitter, FaYoutube } from "react-icons/fa";

const Footer = () => {
  return (
    <div className="pb-[2rem] pt-[7rem] bg-gray-100">
      <div className="w-[80%] mx-auto items-start grid grid-cols-1 pb-[2rem] md:grid-cols-2 lg:grid-cols-4 gap-[3rem]">
        {/* col 1 */}
        <div>
          <h1 className="footer__heading">Support</h1>
          <div>
            <a className="footer__link" href="#">
              Help Center
            </a>
            <a className="footer__link" href="#">
              Safety Information
            </a>
            <a className="footer__link" href="#">
              Cancellation Option
            </a>
            <a className="footer__link" href="#">
              Medical Doctor
            </a>
          </div>
        </div>
        {/* col 2 */}
        <div>
          <h1 className="footer__heading">Company</h1>
          <div>
            <a className="footer__link" href="#">
              About Us
            </a>
            <a className="footer__link" href="#">
              Comunity Blog
            </a>
            <a className="footer__link" href="#">
              Careers
            </a>
            <a className="footer__link" href="#">
              Privacy
            </a>
            <a className="footer__link" href="#">
              Services
            </a>
          </div>
        </div>
        {/* col 3 */}
        <div>
          <h1 className="footer__heading">Contact</h1>
          <div>
            <a className="footer__link" href="#">
              Partnerships
            </a>
            <a className="footer__link" href="#">
              FAQ
            </a>
            <a className="footer__link" href="#">
              Get In Touch
            </a>
          </div>
        </div>
        {/* col 4 */}
        <div>
          <h1 className="footer__heading">Social</h1>
          <div className="flex items-center space-x-4 text-white text-[1.3rem]">
            <div className="footer__icon bg-[#0165e1]">
              <FaFacebookF />
            </div>
            <div className="footer__icon bg-[#cd486b]">
              <FaInstagram />
            </div>
            <div className="footer__icon bg-[#1da1f2]">
              <FaTwitter />
            </div>
            <div className="footer__icon bg-[#cd201f    ]">
              <FaYoutube />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Footer;
