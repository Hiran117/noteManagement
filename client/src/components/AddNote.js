import React, { useState } from 'react';
import axios from 'axios';

function AddNote() {
    const [formData, setFormData] = useState({
        title: '',
        description: ''
    });

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

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
        
        const userId = localStorage.getItem('UserId'); // <-- Retrieving UserId here
        
        try {
            await axios.post('http://localhost:5000/api/notes', { ...formData, UserId: userId }); // <-- Including UserId in request
            setFormData({ title: '', description: '' });
        } catch (err) {
            if (err.response && err.response.data && err.response.data.message) {
                setError(err.response.data.message);
            } else {
                setError('Error adding note');
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="p-4">
            {error && <p className="text-red-500 mb-2">{error}</p>}
            {loading ? (
                <div className="flex justify-center items-center">
                    <div className="animate-spin rounded-full h-5 w-5 border-t-2 border-blue-500"></div>
                </div>
            ) : (
                <div>
                    <h2>Add Note</h2>
                    <form onSubmit={handleSubmit}>
                        <input name="title" placeholder="Title" value={formData.title} onChange={handleChange} />
                        <textarea name="description" placeholder="Description" value={formData.description} onChange={handleChange}></textarea>
                        <button type="submit">Add Note</button>
                    </form>
                </div>
            )}
        </div>
    );
}

export default AddNote;
