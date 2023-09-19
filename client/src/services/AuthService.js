import jwtDecode from 'jwt-decode';
import axios from 'axios';

export const storeToken = (token) => {
  localStorage.setItem('token', token);
  const decodedToken = jwtDecode(token);
  localStorage.setItem('UserId', decodedToken.UserId); // <-- Storing UserId here
  axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
};

export const getToken = () => {
  return localStorage.getItem('token');
};

export const removeToken = () => {
  localStorage.removeItem('token');
  delete axios.defaults.headers.common['Authorization'];
};

export const isTokenValid = () => {
  const token = getToken();
  if (!token) return false;

  try {
    
    const decoded = jwtDecode(token);
    const expirationTime = decoded.exp * 1000;
    if (Date.now() >= expirationTime) {
      removeToken();
      return false;
    } else {
      return true;
    }
  } catch (error) {
    console.error("Invalid token:", error);
    removeToken();
    return false;
  }
};
