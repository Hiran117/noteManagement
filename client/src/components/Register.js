import React, { useState } from 'react';
import axios from 'axios';

function Register() {
    const [formData, setFormData] = useState({
        username: '',
        password: '',
        email: '',
        name: ''
    });
    const [loading, setLoading] = useState(false); // For handling the loading state
    const [message, setMessage] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        try {
            const response = await axios.post('http://localhost:5000/users/register', formData);
            setMessage('Registered successfully!');
        } catch (error) {
            setMessage('Error during registration. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="p-4">
            <h2 className="mb-4 text-xl font-bold">Register</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <input className="border p-2 w-full" name="username" placeholder="Username" value={formData.username} onChange={handleChange} />
                <input className="border p-2 w-full" type="password" name="password" placeholder="Password" value={formData.password} onChange={handleChange} />
                <input className="border p-2 w-full" name="email" placeholder="Email" value={formData.email} onChange={handleChange} />
                <input className="border p-2 w-full" name="name" placeholder="Full Name" value={formData.name} onChange={handleChange} />
                <button type="submit" className="bg-blue-500 text-white p-2 w-full mt-4 rounded">{loading ? 'Registering...' : 'Register'}</button>
            </form>
            {message && <p className="mt-4 text-center">{message}</p>}
        </div>
    );
}

export default Register;
