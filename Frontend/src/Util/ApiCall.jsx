import axios from 'axios';
import { endpoint } from '../config';

export async function addEmail(email) {
    return axios.put(`${endpoint}/User/AddUser`, {
        email: email,
        services: ['EpicGames']
    })
    .then(function (response) {
      return response;
    })
    .catch(function (error) {
        return error;
    });
}

export async function unsubUser(uuid) {
    return axios.delete(`${endpoint}/User/RemoveUser/${uuid}`)
    .then(function (response) {
      return response;
    })
    .catch(function (error) {
        return error;
    });
}

