import {
  Navigate,
  RouterProvider,
  createBrowserRouter,
} from "react-router-dom";
import Layout from "./Components/Layout/Layout";
import Home from "./Components/Home/Home";
import Login from "./Components/Login/Login";
import Register from "./Components/Register/Register";
import Cart from "./Components/Cart/Cart";
import Brands from "./Components/Brands/Brands";
import Categories from "./Components/Categories/Categories";
import Products from "./Components/Products/Products";
import NotFound from "./Components/NotFound/NotFound";
import AuthContextProvider from "./context/AuthContext";
import ProtectedRoute from "./Components/ProtectedRoute/ProtectedRoute";
import AuthRoute from "./Components/ProtectedRoute/AuthRoute";
import ProductDetails from "./Components/ProductDetails/ProductDetails";
import { QueryClientProvider } from "react-query";
import Address from "./Components/Address/Address";
import { queryClient } from "./util/http";
import WishList from "./Components/WishList/WishList";
import ForgetPassword from "./Components/ForgetPassword/ForgetPassword";
import VerifyCode from "./Components/VerifyCode/VerifyCode";
import ResetPassword from "./Components/ResetPassword/ResetPassword";
import UserData from "./Components/UserData/UserData";
import ChangePassword from "./Components/ChangePassword/ChangePassword";
import Order from "./Components/Order/Order";

const router = createBrowserRouter([
  {
    path: "",
    element: <Layout />,
    children: [
      { path: "", element: <Navigate to="home" /> },
      {
        path: "home",
        element: (
          // <ProtectedRoute>
          <Home />
          // </ProtectedRoute>
        ),
      },
      {
        path: "login",
        element: (
          <AuthRoute>
            <Login />
          </AuthRoute>
        ),
      },
      {
        path: "register",
        element: (
          <AuthRoute>
            <Register />
          </AuthRoute>
        ),
      },
      {
        path: "cart",
        element: (
          <ProtectedRoute>
            <Cart />
          </ProtectedRoute>
        ),
      },
      {
        path: "brands",
        element: (
          <ProtectedRoute>
            <Brands />
          </ProtectedRoute>
        ),
      },
      {
        path: "wishList",
        element: (
          <ProtectedRoute>
            <WishList />
          </ProtectedRoute>
        ),
      },
      {
        path: "categories",
        element: (
          <ProtectedRoute>
            <Categories />
          </ProtectedRoute>
        ),
      },
      {
        path: "products",
        element: (
          <ProtectedRoute>
            <Products />
          </ProtectedRoute>
        ),
      },
      {
        path: "address",
        element: (
          <ProtectedRoute>
            <Address />
          </ProtectedRoute>
        ),
      },
      {
        path: "ProductDetails/:id",
        element: (
          // <ProtectedRoute>
            <ProductDetails />
          // </ProtectedRoute>
        ),
      },
      {
        path: "order",
        element: (
          <ProtectedRoute>
            <Order />
          </ProtectedRoute>
        ),
      },
      {
        path: "forget-password",
        element: <ForgetPassword />,
      },
      {
        path: "verify-code",
        element: <VerifyCode />,
      },
      { path: "reset-password", element: <ResetPassword /> },
      {
        path: "user-data",
        element: (
          <ProtectedRoute>
            <UserData />
          </ProtectedRoute>
        ),
      },
      {
        path: "change-pass",
        element: (
          <ProtectedRoute>
            <ChangePassword />
          </ProtectedRoute>
        ),
      },
      { path: "*", element: <NotFound /> },
    ],
  },
]);

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthContextProvider>
        <RouterProvider router={router} />
      </AuthContextProvider>
    </QueryClientProvider>
  );
}

export default App;
