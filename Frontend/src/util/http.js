// import axios from "axios";
import axios from "axios";
import Cookies from "js-cookie";
import { QueryClient } from "react-query";

axios.interceptors.request.use(function (config) {
  config.headers.Authorization = "Bearer " + Cookies.get("token");
  config.baseURL = "https://freshcart.runasp.net/api/";
  return config;
});

export const queryClient = new QueryClient();

export async function getProducts(pageParam, { id }) {
  let url = "products";
  if (id) {
    url += "/" + id;
  }
  if (pageParam) {
    url += `?pageNumber=${pageParam}&PageSize=12`;
  }

  const response = await axios.get(url, {
    headers: { "Content-Type": "application/json" },
  });

  if (response.status === null) {
    const error = new Error("An error occurred while fetching the products");
    error.code = response.status;
    throw error;
  }
  const { data } = response;
  return data;
}

export async function getLoggedUserCart() {
  const response = await axios.get("cart").catch((err) => {
    const error = new Error("An error occurred while fetching the cart");
    error.code = err.response.status;
    throw error;
  });

  if (response.status === undefined) {
    const error = new Error("An error occurred while fetching the cart");
    error.code = response.status;
    throw error;
  }

  const { data } = response;

  return data;
}

export async function deleteProduct({ id }) {
  let url = "cart";
  if (id) {
    url += "/" + id;
  }
  const response = await axios.delete(url);

  if (response.status === null) {
    const error = new Error(
      "An error occurred while deleting the cart product"
    );
    error.code = response.status;
    throw error;
  }

  const { data } = response;

  return data;
}

export async function updateProduct({ id, count }) {
  const response = await axios.put(`cart/${id}`, { count });

  if (response.status === null) {
    const error = new Error(
      "An error occurred while deleting the cart product"
    );
    error.code = response.status;
    throw error;
  }

  const { data } = response;

  return data;
}

export async function getCategories(pageParam) {
  const { data, status } = await axios
    .get(`categories?pageNumber=${pageParam}&PageSize=12`)
    .catch((err) => {
      const error = new Error(
        "An error occurred while fetching the categories"
      );
      error.code = err.response.status;
      throw error;
    });

  if (status === undefined) {
    const error = new Error("An error occurred while fetching the cart");
    error.code = status;
    throw error;
  }

  return data;
}

export async function getSubCategories(id) {
  const { data } = await axios.get(`categories/${id}/subcategories`);

  return data;
}

export async function getLoggedUserData() {
  const { data, status } = await axios
    .get("account/getloggeduserdata")
    .catch((err) => {
      const error = new Error(
        "An error occurred while fetching the categories"
      );
      error.code = err.response.status;
      throw error;
    });

  if (status === undefined) {
    const error = new Error("An error occurred while fetching the cart");
    error.code = status;
    throw error;
  }

  return data;
}

export async function getBrands(pageParam = 1) {
  const { data, status } = await axios
    .get(`brands?pageNumber=${pageParam}&PageSize=12`)
    .catch((err) => {
      const error = new Error(err.response.data);
      error.code = err.response.status;
      throw error;
    });

  if (status === undefined) {
    const error = new Error("An error occurred while fetching the cart");
    error.code = status;
    throw error;
  }

  return data;
}

export async function getLoggedUserWishList() {
  const { data, status } = await axios
    .get("wishlist", {
      headers: {
        Authorization: "Bearer " + Cookies.get("token"),
      },
    })
    .catch((err) => {
      const error = new Error(
        "An error occurred while fetching the categories"
      );
      error.code = err.response.status;
      throw error;
    });

  if (status !== 200) {
    const error = new Error("An error occurred while fetching the cart");
    error.code = status;
    throw error;
  }

  return data;
}

export async function deleteProductFromWishList({ id }) {
  const { status, data } = await axios.delete(`wishlist/${id}`).catch((err) => {
    const error = new Error(
      "An error occurred while deleting the product from the wish list"
    );
    error.code = err.response.status;
    throw error;
  });

  if (status !== 200) {
    const error = new Error("An error occurred while fetching the cart");
    error.code = status;
    throw error;
  }

  return data;
}

export async function getLoggedUserOrder() {
  const { data, status } = await axios.get("orders/user").catch((err) => {
    const error = new Error("An error occurred while fetching the orders");
    error.code = err.response.status;
    throw error;
  });

  if (status !== 200) {
    const error = new Error("An error occurred while fetching the order");
    error.code = status;
    throw error;
  }

  return data;
}

export async function submitOrder(shippingAddress) {
  const { data } = await axios
    .post(
      "orders/checkoutsession?url=https://freshcartstore.vercel.app/order",
      shippingAddress
    )
    .catch((err) => {
      const error = new Error(err.response.data);
      error.code = err.response.status;
      throw error;
    });

  window.location.href = data;
}

export async function changePassword(updatePassword) {
  const response = await axios.put(
    "account/updateloggeduserpassword",
    updatePassword
  );
  return response;
}

export async function forgetPassword(data) {
  const response = await axios.post("account/forgetpassword", data);
  return response;
}

export async function signIn(data) {
  const response = await axios.post("account/signin", data);
  return response;
}

export async function addProdToWishlist(data) {
  const response = await axios.post("wishlist", data);
  return response;
}

export async function addProdToCart(data) {
  const response = await axios.post("cart", data);
  return response;
}

export async function signup(data) {
  const response = await axios.post("account/signup", data);
  return response;
}

export async function resetPassword(data) {
  const response = await axios.put("account/resetpassword", data);
  return response;
}

export async function updateUserData(data) {
  const response = await axios.put("account/updateloggeduserdata", data);
  return response;
}

export async function verifyResetCode(data) {
  const response = await axios.post("account/verifyresetcode", data);
  return response;
}
