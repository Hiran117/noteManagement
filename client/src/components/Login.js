import React, { useState } from 'react';
import axios from 'axios';
import { storeToken } from '../services/AuthService';
import { useNavigate } from 'react-router-dom';

function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const Navigate = useNavigate();

    const handleLogin = async () => {
        try {
            const response = await axios.post('http://localhost:5000/users/login', { username, password });
            storeToken(response.data.token);
            Navigate.push('/components/userNotes');
        } catch (err) {
            if (err.response.status === 400) {
                setError('UserId is missing. Please re-login or contact support.'); // <-- Improved error message here
            } else if (err.response && err.response.data && err.response.data.message) {
                setError(err.response.data.message);
            } else {
                setError('Something went wrong. Please try again.');
            }
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50">
            <div className="bg-white p-8 rounded-lg shadow-md w-96">
                <h1 className="text-2xl font-bold mb-4">Login</h1>
                {error && <p className="text-red-500 mb-2">{error}</p>}
                <input 
                    type="text" 
                    placeholder="Username" 
                    className="border p-2 w-full mb-3 rounded" 
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                <input 
                    type="password" 
                    placeholder="Password" 
                    className="border p-2 w-full mb-3 rounded" 
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <button 
                    onClick={handleLogin} 
                    className="bg-blue-500 text-white p-2 w-full rounded hover:bg-blue-600"
                >
                    Login
                </button>
            </div>
        </div>
    );
}

export default Login;
