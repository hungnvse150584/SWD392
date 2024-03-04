import Link from 'next/link';
import React from 'react';

export default function HeaderPartyHost() {
  const data = [
    { id: 1, name: 'Thuan', img: 'https://img3.thuthuatphanmem.vn/uploads/2019/06/13/anh-nen-co-gai-anime-de-thuong_095240876.jpg' },
  ];

  return (
    <div className="w-full bg-red">
      <div className="flex w-11/12 mx-auto items-center justify-between h-12vh">
        <div className="relative w-30 cursor-pointer h-30 lg:w-40 lg:h-40 object-contain">
          <img src="/images/logo.png" alt="logo" layout="fill" />
        </div>
        <div className="flex items-center space-x-12">
          <Link className="nav-link" href="#">
            ADD PRODUCT
          </Link>
          <div className="flex items-center border border-black rounded-lg p-2">
            {data.map(item => (
              <div key={item.id} className="flex items-center space-x-1">
                <p className='text-[16px]'>Hello {item.name}</p>
                <img src={item.img} alt={item.name} className="w-12 h-12 lg:w-14 lg:h-14 object-cover rounded-full" />
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

    // const ItemsPage = () => {
    //     const [items, setItems] = useState([]);
      
    //     useEffect(() => {
    //       const fetchData = async () => {
    //         const response = await fetch('');
    //         const data = await response.json();
    //         setItems(data);
    //       };
      
    //       fetchData();
    //     }, []);