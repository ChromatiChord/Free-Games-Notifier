import { Button, TextField, Typography, Box } from '@mui/material';
import { useState } from 'react';

export default function Form() {

    const [email, setEmail] = useState('');
    const [token, setToken] = useState('');

    function submit() {
        console.log(`Email: ${email}, Token: ${token}`);
    }

    return (
      <Box 
        display="flex" 
        flexDirection="column" 
        justifyContent="center" 
        alignItems="center" 
        textAlign="center" 
      >
        <Typography sx={{ marginBottom: 2 }}>
          Enter in your email and token below to sign up!
        </Typography>
        <TextField id="email" label="Email" variant="outlined" onChange={e => setEmail(e.target.value)} sx={{ marginBottom: 2 }}/>
        <TextField id="token" label="Password Token" variant="outlined" onChange={e => setToken(e.target.value)} sx={{ marginBottom: 2 }}/>
        <Button variant="contained" onClick={submit}>Submit</Button>
      </Box>
    )
}
