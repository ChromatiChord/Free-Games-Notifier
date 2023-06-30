import axios from 'axios';
import { endpoint } from './config';

export async function addEmail({email, token}) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    return axios.put(`${endpoint}/Email/AddEmail`, {
        email
    })
    .then(function (response) {
      return response;
    })
    .catch(function (error) {
        return error;
    });
}