import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Login from './components/Login';
import Register from './components/Register';
import UserNotes from './components/UserNotes';
import AddNote from './components/AddNote';
import EditNote from './components/EditNote';
import Navbar from './components/Navbar';

function App() {
  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/UserNotes" element={<UserNotes />} />
        <Route path="/AddNote" element={<AddNote />} />
        <Route path="/EditNote" element={<EditNote />} />
        <Route path="/" element={<Login />} />
      </Routes>
    </>
  );
}

export default App;
