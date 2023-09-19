import React, { useState, useEffect } from 'react';
import axios from 'axios';

function EditNote({ noteId }) {
    const [formData, setFormData] = useState({
        title: '',
        description: ''
    });
    const [error, setError] = useState(null);  // Add this line for error handling

    useEffect(() => {
        const fetchNoteDetails = async () => {
            try {
                const response = await axios.get(`http://localhost:5000/api/notes/${noteId}`);
                setFormData(response.data);
            } catch (err) {
                if (err.response && err.response.data && err.response.data.message) {
                    setError(err.response.data.message);
                } else {
                    setError('Error fetching note details');
                }
            }
        };
        fetchNoteDetails();
    }, [noteId]);

    const handleChange = (e) => { /* ... existing code ... */ };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.put(`/api/notes/${noteId}`, formData);
            alert('Note updated successfully!');
        } catch (err) {
            if (err.response && err.response.data && err.response.data.message) {
                setError(err.response.data.message);
            } else {
                setError('Error updating note');
            }
        }
    };

    return (
        <div>
            {error && <p>{error}</p>}
            {/* ... existing code ... */}
        </div>
    );
}
export default EditNote;