import TitleBar from "../components/TitleBar"
import SubTitle from '../components/SubTitle';
import Inputer from "../components/Inputer";
import Button from "../components/Button";
import { BottomShifter } from "../components/BottomShifter";
// import Signup from "./Signup";
// import { useNaviagte } from 'react-router-dom'
import { useNavigate } from "react-router-dom"
import axios from 'axios';
import { useState } from "react";
export default function Signin() {
  const navigate = useNavigate();
  //Try copying the styles of signup or signin into one another, so to maintain the same styling
  const [result, setResult] = useState(false);
  const [resContent, setResultContent] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  if (!result) {
    return <div className="flex bg-gray-300 justify-center items-center w-screen h-screen border ">
      <div className="border rounded-lg w-96 h-2/4 bg-white flex flex-col justify-around ">
        <div className="flex  flex-col items-center  w-full h-auto">
          <TitleBar label='Sign In' />
          <SubTitle label='Enter your credentials to access your account' />
        </div>
        <div className="ml-4 flex flex-col justify-between h-72 start w-full">
          <Inputer label='Email' onChange={e => setUsername(e.target.value)} />
          <Inputer label='Password' onChange={e => setPassword(e.target.value)} />
          <Button label='Sign In' onPress={async () => {
            //try changing the variable type to let/var if error occurs 
            
            try {
              let response = await axios.post(
                'http://localhost:5111/api/v1/user/signin',
                {
                  username,  // Assuming `username` and `password` are defined
                  password
                },
                {
                  maxContentLength: Infinity,  // No size limit for content length
                  maxBodyLength: Infinity      // No size limit for request body length
                }
              );
              if (response.status == 200 || response.status == 201) {
                console.log(response.data.token);
                localStorage.setItem('authorization', response.data.token);
                navigate('/dashboard');
              }
            }
            catch (e) {
              console.log('Inside catch - some error occurred with backend'+e.message);
              setResultContent('some error occurred with backend');
              // setResultContent('Some error occurred,so try later again'); //Generic result
              setResult(true);
            }
          }} />
          <BottomShifter label="Don't have an account?" linkContext='signup' to='/signup' />
        </div>
      </div>
    </div>
  }
  else {
    return <div className="flex w-screen flex-col items-center h-screen justify-center">
      <div className="text-3xl ">{resContent}</div>
      <BottomShifter label="Don't have an account?" className='border-4 border-white-500' linkContext='signup' to='/signup' />
    </div>
  }
}