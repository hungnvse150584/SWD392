import React from 'react'
import Footer from '@/Components/Footer'
import Header from '@/Components/HeaderPartyHost'
import AddProductPartyHost from '@/Components/addproduct'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
export default function newproduct() {
  return (
    <>
    <div className="sticky top-0 bg-white shadow z-50">
        <Header />
      </div>
    <p className='text-[40px] text-center'>ADD Product</p>
    <AddProductPartyHost/>
    <Footer/>
    <ToastContainer/>
    </>
  )
}
