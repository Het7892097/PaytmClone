import axios from 'axios';
import jwt from 'jsonwebtoken';
const token=jwt.sign({userId:'66c353f68629ed01f31a5b3a'},'NotYours');
console.log(token)
async function dataFetcher() {
    const accResponse = await axios.get('http://localhost:3000/api/v1/account/balance', {
        headers: {
            authorization:'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2NmJhNDFiMWU4NWY3MGFjZDYwMWExMWYiLCJpYXQiOjE3MjQwODc5NjZ9.CrVGb9sg0Vw8fDF4E51BtNP-FQcYfyjUyRDHxdj24O0'
        }
    });
    console.log(accResponse.data);
}
dataFetcher();