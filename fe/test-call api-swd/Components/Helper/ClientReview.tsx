/* eslint-disable react/no-unescaped-entities */
import React from "react";
import Image from "next/image";
import { StarIcon } from "@heroicons/react/16/solid";

interface Props {
  image: string;
  name: string;
}

const ClientReview = ({ image, name }: Props) => {
  return (
    <div className="p-4">
      <div>
        <Image
          src={`${image}`}
          alt={name}
          width={80}
          height={80}
          className="rounded-full mx-auto"
        />
      </div>
      <p className="mt-[2rem] text-center text-[16px] text-black opacity-60">
        "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quaerat, fuga.
        Quo, error cupiditate! Libero voluptate delectus aliquid odio eveniet
        exercitationem temporibus natus, illo saepe totam molestias magnam"
      </p>
      <div className="mt-[2rem]">
        <div className="flex w-[100%] text-center justify-center items-center space-x-1">
            <StarIcon className="w-[1rem] h-[1rem] text-orange-500"/>
            <StarIcon className="w-[1rem] h-[1rem] text-orange-500"/>
            <StarIcon className="w-[1rem] h-[1rem] text-orange-500"/>
            <StarIcon className="w-[1rem] h-[1rem] text-orange-500"/>
            <StarIcon className="w-[1rem] h-[1rem] text-orange-500"/>
        </div>
        <h1 className="mt-[0.5rem] font-bold text-center text-[18px] text-black mb-[0.3rem]">{name}</h1>
        <p className="text-center text-black text-[15px] opacity-70">Traveler</p>
      </div>
    </div>
  );
};

export default ClientReview;
