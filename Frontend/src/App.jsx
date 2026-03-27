import { useState } from 'react';
import './App.css';

function App() {
  const [searchQuery, setSearchQuery] = useState('');
  const [repositories, setRepositories] = useState([]);
  
  const [isPopupOpen, setIsPopupOpen] = useState(false);
  const [selectedRepo, setSelectedRepo] = useState(null);
  const [emailInput, setEmailInput] = useState('');

  // server address
  const API_BASE_URL = 'http://localhost:5140/api/github';

  const handleSearch = async () => {
    if (!searchQuery) return;
    try {
      const response = await fetch(`https://api.github.com/search/repositories?q=${searchQuery}`);
      const data = await response.json();
      setRepositories(data.items || []); 
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  // send it to the backend
  const handleBookmark = async (repo) => {
    try {
      const response = await fetch(`${API_BASE_URL}/bookmark`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(repo), 
      });

      if (response.ok) {
        alert(`Success! "${repo.name}" has been bookmarked.`);
      } else {
        alert('Failed to bookmark the repository.');
      }
    } catch (error) {
      console.error('Error saving bookmark:', error);
      alert('Error connecting to the server.');
    }
  };

  const openEmailPopup = (repo) => {
    setSelectedRepo(repo);
    setIsPopupOpen(true);
  };

  const handleSendEmail = () => {
    alert(`Sending email to ${emailInput} about ${selectedRepo.name}...`);
    setIsPopupOpen(false);
    setEmailInput('');
  };

  return (
    <div className="App">
      <h1>GitHub Search</h1>
      
      <div className="search-container">
        <input 
          type="text" 
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          onKeyDown={(e) => e.key === 'Enter' && handleSearch()}
          placeholder="Type repository name..."
        />
        <button onClick={handleSearch}>Search</button>
      </div>

      <div className="gallery">
        {repositories.map((repo) => (
          <div key={repo.id} className="card">
            <img src={repo.owner.avatar_url} alt="avatar" className="avatar" />
            <h3>{repo.name}</h3>
            <div className="card-actions">
              <button className="btn-bookmark" onClick={() => handleBookmark(repo)}>
                Bookmark
              </button>
              <button className="btn-email" onClick={() => openEmailPopup(repo)}>
                Send Email
              </button>
            </div>
          </div>
        ))}
      </div>

      {isPopupOpen && (
        <div className="popup-overlay">
          <div className="popup-content">
            <h3>Send info about {selectedRepo.name}</h3>
            <input 
              type="email" 
              placeholder="Enter email address" 
              value={emailInput}
              onChange={(e) => setEmailInput(e.target.value)}
            />
            <div className="popup-buttons">
              <button onClick={handleSendEmail} className="btn-send">Send</button>
              <button onClick={() => setIsPopupOpen(false)} className="btn-cancel">Cancel</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default App;