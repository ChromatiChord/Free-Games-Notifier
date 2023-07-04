import { Container, Box } from '@mui/material';
import FormPage from './Pages/FormPage';
import {
  Routes,
  BrowserRouter as Router,
  Route
} from "react-router-dom";


import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import UnsubPage from './Pages/UnsubPage';

function App() {
  return (
    <Box sx={{ bgcolor: 'black', height: '100vh' }}>
      <ToastContainer />
      <Container 
        maxWidth='xs'
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}>
        <Router>
          <Routes>
            <Route path="/Free-Games-Notifier" element={<FormPage />}/>
            <Route path="/Free-Games-Notifier/unsub/:uuid" element={<UnsubPage />}/>
          </Routes>
        </Router>
      </Container>
    </Box>
  );
}

export default App;
