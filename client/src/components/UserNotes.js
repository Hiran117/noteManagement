// src // components // UserNotes.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';

function UserNotes() {
    const [notes, setNotes] = useState([]);
    const [error, setError] = useState(null);  // Add this line for error handling

    useEffect(() => {
        const fetchNotes = async () => {
            try {
                const response = await axios.get('http://localhost:5000/api/notes');
                setNotes(response.data);
            } catch (err) {
                if (err.response && err.response.data && err.response.data.message) {
                    setError(err.response.data.message);
                } else {
                    setError('Error fetching notes');
                }
            }
        };
        fetchNotes();
    }, []);

    const handleDelete = async (noteId) => {
        try {
            await axios.delete(`/api/notes/${noteId}`);
            setNotes(prevNotes => prevNotes.filter(note => note.id !== noteId));
        } catch (err) {
            if (err.response && err.response.data && err.response.data.message) {
                setError(err.response.data.message);
            } else {
                setError('Error deleting note');
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
export default UserNotes;