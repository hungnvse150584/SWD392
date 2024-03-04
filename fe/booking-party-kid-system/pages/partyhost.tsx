import React from 'react'
import ListProduct from '@/Components/Listproduct'
import Footer from '@/Components/Footer'
import HederPartyHost from "@/Components/HeaderPartyHost"
export default function partyhost() {
  return (
    <>
    <HederPartyHost/>
    <h1 className='text-center text-[45px]'>List Product</h1>
    <ListProduct/>
    <Footer/>
   </>
  )
}
