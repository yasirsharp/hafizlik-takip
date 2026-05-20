const axios = require('axios');

const API_BASE_URL = 'http://localhost:5190/api';

async function runTest() {
  console.log('--- API CONNECTION TEST ---');
  try {
    const registerResponse = await axios.post(API_BASE_URL + '/auth/register', {
      email: 'testuser2@example.com',
      password: 'Password123!',
      firstName: 'Test',
      lastName: 'User'
    });
    console.log('Register Success:', registerResponse.data.success);
    
    const loginResponse = await axios.post(API_BASE_URL + '/auth/login', {
      email: 'testuser2@example.com',
      password: 'Password123!'
    });
    console.log('Login Success:', loginResponse.data.success);
    console.log('Token received:', loginResponse.data.data.token ? 'YES' : 'NO');
    
  } catch (error) {
    if (error.response) {
      console.error('API Error:', error.response.data);
    } else {
      console.error('Request Error:', error.message);
    }
  }
}

runTest();
