import SubTitle from "../components/SubTitle"
import TitleBar from "../components/TitleBar"
import Inputer from '../components/Inputer'
import { BottomShifter } from "../components/BottomShifter"
import Button from "../components/Button"
import Signin from "./Signin"
import { Link } from "react-router-dom"
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import axios from 'axios';
// import  from 'axios';

export default function Signup() {
   const navigate = useNavigate();
   const [result, setResult] = useState(false);
   const [resContent, setResultContent] = useState('gibberish');
   const [lastName, setLastName] = useState('');
   const [firstName, setFirstName] = useState('');
   const [username, setUsername] = useState('');
   const [password, setPassword] = useState('');
   if (!result) {
      return <div className=" mt-5 flex flex-col bg-grey-300 items-center justify-center w-screen h-screen border">
         <div className="border rounded-lg w-96 h-2/4 bg-white  flex flex-col justify-around">
            <div className="text-center ">
               <TitleBar label='Signup' />
               <SubTitle label='Enter your information to create an account' />
            </div>
            <div className="ml-4 flex flex-col justify-between h-72">
               <Inputer label='First Name' textHolder='your name' onChange={(e) => {
                  setFirstName(e.target.value);
               }} />
               <Inputer label='Last Name' textHolder='your surname' onChange={(e) => setLastName(e.target.value)} />
               <Inputer label='Email' textHolder='your email-address or username' onChange={(e) => {
                  setUsername(e.target.value);
               }} />
               <Inputer label='Password' textHolder='your password' onChange={e => setPassword(e.target.value)} />
               {/* Try adding an me components which checks whether the user is logged in or not whenever user tries to route */}
               <Button label='Signup' onPress={async() => {
                  // alert('Hello mofos') for debugging
                try{  let response = await axios.post('http://localhost:5111/api/v1/user/signup', {
                     firstName,//The js allows not to assign values in object, if the key-value pairs are same/equal
                     lastName,
                     username,
                     password
                  })
                  console.log(response.data);// for debugging purpose
                  if (response.status == 201 || response.status == 200) {
                     localStorage.setItem('authorization', response.data.token);
                     navigate('/DashBoard');
                  }
                  else {
                     setResultContent(response.data.message);
                     console.log(response.data.message);
                     // setResultContent('Incorrect user-details or server-problem')// generic message for all faiilures
                     setResult(true);
                  }}
                  catch(e){
                     console.log('Some error occurred while with backend')
                     setResultContent('Some error occurred while with backend');
                     // setResultContent('Incorrect user-details or server-problem')// generic message for all faiilures
                     setResult(true);
                  }
               }} />
               <BottomShifter linkContext='signin' label='Already have an account' to='/signin' />
            </div>
         </div>
      </div>
   }
   else {
      return <div className="flex w-screen flex-col items-center h-screen justify-center">
        <div className="text-3xl ">{resContent}</div>
        <BottomShifter label="Already have an account? try"className='border-4 border-white-500' linkContext='signin' to='/signin' />
      </div>
    }
}