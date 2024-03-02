// import Login from "@/Components/Login"

// const LoginPage = () => {

//   return (
//     <div className="overflow-x-hidden">

//       <Login/>
//     </div>
//   );
// };

// export default LoginPage;

import Login from "@/Components/Login";
import api from "./services/api";
import React, { useEffect } from "react";
import Footer from "../Components/Footer";

const LoginPage: React.FC = () => {
  useEffect(() => {
    const fetchData = async () => {
      try {
        // const response = await api.get("https://localhost:7286/api/Rooms");
        const response = await api.get("https://dummyjson.com/products");
        console.log("Data:", response.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  });

  return (
    <div className="overflow-x-hidden">
      <Login />
    </div>
  );
};

export default LoginPage;
