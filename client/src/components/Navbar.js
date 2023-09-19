import { useNavigate } from 'react-router-dom';

function Navbar() {
    const Navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        Navigate.push('/login');  // Redirecting to Login page after logout
    };

    return (
        <nav className="bg-blue-500 p-4">
            <ul className="flex space-x-4">
                {/* ... other links */}
                <li>
                    <button onClick={handleLogout} className="bg-red-600 text-white p-2 rounded hover:bg-red-700">
                        Logout
                    </button>
                </li>
            </ul>
        </nav>
    );
}

export default Navbar;
