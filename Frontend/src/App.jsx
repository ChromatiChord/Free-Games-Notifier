import { Container, Box } from '@mui/material';
import Form from './Form';
import { imageSource } from './config';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <Box sx={{ bgcolor: 'black', height: '100vh' }}>
      <ToastContainer />
      <Container 
        maxWidth='sm'
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}>
        <Box sx={{ marginBottom: 2 }}>
          <img src={imageSource} alt="Epic Games Logo"  height="170vh"/>
        </Box>
        <Form />
      </Container>
    </Box>
  );
}

export default App;
