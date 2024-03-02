/* eslint-disable react/no-unescaped-entities */
import React from "react";

const login = () => {
  return (
    <div className="w-full h-screen flex items-start">
      <div className="relative w-1/2 h-full flex flex-col">
        <div className="absolute top-[20%] left-[10%] flex flex-col">
          <h1 className="text-4xl text-white font-bold my-4">
            Login your account
          </h1>
          <p></p>
        </div>

        <img src="/images/d5.png" className="w-full h-full object-cover" />
      </div>

      <div className="w-1/2 h-full bg-white flex flex-col p-20 justify-between">
        <h1 className="text-xl text-black font-semibold">Interactive Brand</h1>

        <div className="w-full flex flex-col max-w-[550px]">
          <div className="w-full flex flex-col mb-5">
            <h3 className="text-3xl font-semibold mb-2">Login</h3>
            <p className="text-base mb-2">Welcome us party</p>
          </div>

          <div className="w-full flex flex-col">
            <input
              type="Email"
              placeholder="Email"
              className="w-full text-black py-2 my-2 bg-transparent border-b border-black outline-none focus:outline-none"
            />
            <input
              type="password"
              placeholder="Password"
              className="w-full text-black py-2 my-2 bg-transparent border-b border-black outline-none focus:outline-none"
            />
          </div>

          <div className="w-full flex items-center justify-between ">
            <div className="w-full flex items-center">
              <input type="checkbox" className="w-4 h-4 mr-2" />
              <p className="text-sm">Remember me</p>
            </div>

            <p className="text-sm font-medium whitespace-nowrap cursor-pointer underline underline-offset-2">
              Forgot password ?
            </p>
          </div>

          <div className="w-full flex flex-col my-4">
            <button className="w-full text-white my-2 font-semibold bg-black rounded-md p-4 text-center flex items-center justify-center">
              Login
            </button>
            <button className="w-full text-black my-2 font-semibold bg-white border-2 border-black rounded-md p-4 text-center flex items-center justify-center">
              Sign Up
            </button>
          </div>
        </div>

        <div className="w-full flex items-center justify-center">
          <p className="text-sm font-normal text-black">
            Don't have an account?{" "}
            <span className="font-semibold underline underline-offset-2 cursor-pointer">
              Sign up for free
            </span>
          </p>
        </div>
      </div>
    </div>
  );
};

export default login;
