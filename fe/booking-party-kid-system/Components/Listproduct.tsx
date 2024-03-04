'use client'
import React, { useEffect, useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
export default function Listproduct() {
    // const [products, setProducts] = useState([
    //     { ProductId:1, PartyHostId: 1, ProductName: 'Product 1', ProductUrl: 'https://64.media.tumblr.com/1c70eb17ebf939fc274db9f0511e4e84/e598792538d8d8b4-3e/s540x810/3af21ef570426d9d3168ec5d727cd26680ab7e54.jpg', ProductType: 'Type 1', ProductStyle: 'Style 1', Price: '10', ProductStatus: '' },
    //     { ProductId:2, PartyHostId: 2, ProductName: 'Product 2', ProductUrl: 'https://wallpapercave.com/wp/wp12026947.jpg', ProductType: 'Type 2', ProductStyle: 'Style 2', Price: '20', ProductStatus: 'Active' },
    //     { ProductId:3, PartyHostId: 3, ProductName: 'Product 3', ProductUrl: 'https://64.media.tumblr.com/1c70eb17ebf939fc274db9f0511e4e84/e598792538d8d8b4-3e/s540x810/3af21ef570426d9d3168ec5d727cd26680ab7e54.jpg', ProductType: 'Type 3', ProductStyle: 'Style 3', Price: '30', ProductStatus: 'Active' },
    //     { ProductId:4, PartyHostId: 1, ProductName: 'Product 1', ProductUrl: 'https://64.media.tumblr.com/1c70eb17ebf939fc274db9f0511e4e84/e598792538d8d8b4-3e/s540x810/3af21ef570426d9d3168ec5d727cd26680ab7e54.jpg', ProductType: 'Type 1', ProductStyle: 'Style 1', Price: '10', ProductStatus: 'Active' },
    //     { ProductId:5, PartyHostId: 2, ProductName: 'Product 2', ProductUrl: 'https://tse2.mm.bing.net/th?id=OIP.Daqyoa_9qNPi6c4AOyGRuwHaEK&pid=Api&P=0&h=220', ProductType: 'Type 2', ProductStyle: 'Style 2', Price: '20', ProductStatus: 'Active' },
    //     { ProductId:6, PartyHostId: 3, ProductName: 'Product 3', ProductUrl: 'https://64.media.tumblr.com/1c70eb17ebf939fc274db9f0511e4e84/e598792538d8d8b4-3e/s540x810/3af21ef570426d9d3168ec5d727cd26680ab7e54.jpg', ProductType: 'Type 3', ProductStyle: 'Style 3', Price: '30', ProductStatus: 'Active' }
    // ]);


    const [products, setProducts] = useState([]);
    const [selectedProduct, setSelectedProduct] = useState(null);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const res = await fetch("https://65e3e19488c4088649f610c0.mockapi.io/api/v1/products",{
                method:'Get'
            });
            if (!res.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await res.json();
            setProducts(data); // Set fetched data to the state
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    // const handleDelete = (productId) => {
    //     setProducts(products.filter(product => product.ProductId !== productId));
    // };


    const handleDelete = async (productId) => {
        try {
            const res = await fetch(`https://65e3e19488c4088649f610c0.mockapi.io/api/v1/products/${productId}`, {
                method: 'DELETE'
            });
            if (!res.ok) {
                throw new Error('Failed to delete product');
            }
            // Remove the deleted product from the state
            setProducts(products.filter(product => product.ProductId !== productId));
            setSelectedProduct(null); // Deselect the product
            toast.success("Delete product succes");
        } catch (error) {
            console.error('Error deleting product:', error);
            toast.error("Delete product fail");
        }
    };

    return (
    <div>
            <ToastContainer/>
        <div className='grid grid-cols-6'>
            <div className='bg-blue-300 col-span-1 mt-[27px]'>

            </div>
        <div className='grid grid-cols-4 mt-4 col-span-5'>
            {products.length === 0 ? (
                <div className="col-span-5 flex justify-center items-center h-screen">
                    <p className='text-center text-2xl'>No products</p>
                </div>
            ) : (
                products.map(item => (
                    <div key={item.ProductId} className={`mt-3 ml-6 mb-2 border border-black p-4 rounded-[30px] ${item.ProductStatus === 'Active' ? 'bg-green-100' : 'bg-red-100'}`}>
                        <img className='rounded-[30px] w-[45vw] h-[30vh] rounded-[10%]' src={item.ProductUrl} alt={`Product ${item.ProductName}`} />
                        <p className='text-center text-3xl'>{item.ProductName}</p>
                        <p>Type: {item.ProductType}</p>
                        <p>Style: {"#" + item.ProductStyle}</p>
                        <p className='text-right'>{item.Price + "$"}</p>
                        <p className='hidden'>Product Status: {item.ProductStatus}</p>
                        <div className="flex justify-between">
                            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Update</button>
                            <button onClick={() => setSelectedProduct(item)} className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">Delete</button>
                        </div>
                    </div>
                ))
            )}

            {selectedProduct && (
                <div className="fixed inset-0 flex items-center justify-center bg-gray-500 bg-opacity-75">
                    <div className="bg-white p-8 rounded-lg">
                        <h2 className="text-xl mb-4">Confirm Delete</h2>
                        <p>Are you sure you want to delete product {selectedProduct.ProductName}?</p>
                        <div className="flex justify-end mt-4">
                            <button onClick={() => setSelectedProduct(null)} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded mr-2">Cancel</button>
                            <button onClick={() => handleDelete(selectedProduct.ProductId)} className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">Delete</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
        </div>
    </div>
    );
}
