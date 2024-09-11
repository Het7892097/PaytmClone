import axios from "axios";

const currentUser=await axios.post('http://localhost:5111/api/v1/user/signin',{
    "username": "het41664@gmail.com",
    "password": "Het$7920"
  });
 
  console.log(currentUser.data.token);