import React, { useState } from "react";
import { useQuery } from "react-query";
import {
  getAllOrders,
  getAllProducts,
  getLoggedUserOrder,
  getOrders,
  getProducts,
} from "../../util/http";
import { useEffect } from "react";

const Stat = () => {
  const [count, setCount] = useState(0);
  const [avg, setAvg] = useState(0);
  const [totalSales, setTotalSales] = useState(0);

  const claclAverage = (arr) => {
    let sum = 0;
    arr?.forEach((element) => {
      sum += element.price;
    });
    return sum / arr?.length;
  };

  const calcTotalSales = (arr)=>{
    let sum = 0;
    arr?.forEach((element) => {
        element?.orderItems.forEach((item) => {
          sum += item.price;
        });
    //   sum += element.price;
    });
    setTotalSales(sum);
    return sum;
  }

  useEffect(() => {
    async function getData() {
      const response1 = await getAllProducts();
      setCount(response1.length);
      setAvg(claclAverage(response1));
      const response2 = await getAllOrders();
      setTotalSales(calcTotalSales(response2))

    }

    getData();
  });
  // const { data, isLoading: isProductLoading } = useQuery({
  //   queryKey: ["productsAll"],
  //   queryFn:  getAllProducts,
  //   onSuccess:()=>{
  //       claclAverage();
  //   }
  // });

  // console.log(data);
  // const claclAverage = () => {
  //   let sum = 0;
  //   data?.forEach((element) => {
  //     sum += element.price;
  //   });
  //   setAvg(sum / data?.length);
  //   return sum / data?.length;
  // };
  //   if (!isProductLoading) {
  //     setAvg(claclAverage());
  //   }
  // const { data: order, isLoading } = useQuery({
  //   queryKey: ["wishlist"],
  //   queryFn: getAllOrders,
  //   onSuccess:()=>{
  //     console.log(order);
  //   setTotalSales(calcTotalSales());

  //   }
  // });
  // const calcTotalSales = ()=>{
  //   let sum = 0;
  //   order?.forEach((element) => {
  //       element?.orderItems.forEach((item) => {
  //         sum += item.price;
  //       });
  //   //   sum += element.price;
  //   });
  //   setTotalSales(sum);
  //   return sum;
  // }

  const { data: xx } = useQuery({
    queryFn: getOrders,
    queryKey: ["dd"],
  });
  // console.log(xx);
  return (
    <div>
      <h1>Statistics</h1>
      <h2>Total Records: {count}</h2>

      <h2>Average Price: {avg} EGP</h2>
      <h2>Total Sales: {totalSales}</h2>

      <table className="table table-hover">
        <thead>
          <tr>
            <th>Category</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>
          {xx?.map((category, index) => (
            <tr key={index}>
              <td>{category.name}</td>
              <td>{category.totalPrice}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Stat;
