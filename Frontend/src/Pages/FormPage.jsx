import { Button, TextField, Typography, Box } from '@mui/material';
import { useState } from 'react';
import { addEmail } from '../Util/ApiCall';
import { generateToast } from '../Util/ToastGenerator';
import { imageSource } from '../config';

const emailRegexValidator = /^[a-zA-Z0-9.-]+@([a-zA-Z0-9-]+.)+[a-zA-Z]{2,4}$/;

const inputFieldStyle = { 
  marginBottom: 2, 
  color: 'white', 
  borderColor: 'white', 
  '& label': { color: 'white' }, 
  '& .MuiOutlinedInput-notchedOutline': { borderColor: 'white' }, 
  '& .MuiInputBase-input': { color: 'white' } 
}

export default function FormPage() {

    const [email, setEmail] = useState('');
    // const [token, setToken] = useState('');

    function submit() {
        if (!emailRegexValidator.test(email)) {
          generateToast('Not a valid email address!', 'error')
        } else {
          addEmail(email).then((response) => {
            console.log(response);
            if (response.status === 200) {
              generateToast('Email added!', 'success');
            } else if (response.response.status === 409) {
              generateToast('Email already registered!', 'error');
            }
            else {
              generateToast('Error adding email', 'error');
            }
          });
        }
    }

    function onKeyDownHandle(e) {
      if (e.key === 'Enter') { 
        submit();  
      }
    }

    return (
      <>
        <Box sx={{ marginBottom: 2 }}>
          <img src={imageSource} alt="Epic Games Logo"  height="170vh"/>
        </Box>
        <Box 
          display="flex" 
          flexDirection="column" 
          justifyContent="center" 
          alignItems="center" 
          textAlign="center" 
        >
          <Typography color="white" sx={{ marginBottom: 2 }}>
            Never miss a free game from Epic Games again!
          </Typography>
          <Typography color="white" sx={{ marginBottom: 2 }}>
            Subscribe below for alerts on new free games.
          </Typography>
          <TextField 
          id="email" 
          label="Your Email" 
          variant="outlined" 
          onChange={e => setEmail(e.target.value)} 
          sx={inputFieldStyle}
          onKeyDown={onKeyDownHandle}/>
          {/* <TextField id="token" label="Password Token" variant="outlined" onChange={e => setToken(e.target.value)} sx={inputFieldStyle}/> */}
          <Button variant="contained" onClick={submit} sx={{ color: 'white', backgroundColor: '#333' }}>Submit</Button>
        </Box>
      </>
    )
}
