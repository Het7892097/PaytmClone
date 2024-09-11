import Inputer from "../components/Inputer";
import User from '../components/User'
import { useEffect, useState } from "react";
import axios from 'axios';
import { useNavigate } from "react-router-dom";
// import jwt from 'jsonwebtoken'; //It's prefered not to use the NodeJs module in browser(frontend).
// As in our case that node_module is jwt. Even if we want to use it, we can use it by manually importing using polyfill
export default function DashBoard() {
    const [userList, setUserList] = useState([]);
    const [currentUser,setCurrentUser]=useState({});
    const [username,setUsername]=useState('');
    const [balance,setBalance]=useState(0);

    const naviagate=useNavigate();

    function currentUserFilterer(userList,current_user_Id){
        let userIndex=-1; //Initially set to -1, so if the target cuurent user is not present, results into error 
        for(let i=0;i<userList.length;++i){
            if(userList[i]._id==current_user_Id)
            {
                userIndex=i;
                break;
            }
        }
        const currentUser=userList.splice(userIndex,1);
        // const filteredUserList=userList;
        // console.log(JSON.stringify(currentUser));
        return [currentUser[0],userList];

    }
    useEffect(() => {
        async function userFetcher() {
          const currentUserToken = localStorage.getItem('authorization');
      
          const response = await axios.get('http://localhost:3000/api/v1/user/bulk');
          const users = response.data.user;
        
          const currentTokenedUser=await axios.post('http://localhost:3000/api/v1/user/detailer',{
            token:currentUserToken
          });
          console.log(currentTokenedUser.data);
          const accResponse = await axios.get('http://localhost:3000/api/v1/account/balance', {
            headers: {
              authorization: currentUserToken
            }
          });
          console.log("rendered: before filtering user");
          console.log(currentTokenedUser.data._id);
          const [currentFilteredUser, filteredUserList] = currentUserFilterer(users, currentTokenedUser.data._id);
          if (currentFilteredUser) {
            // Check if username needs to be updated
            if (username !== currentFilteredUser.firstName) {
            //   console.log(username); // Debug log
              setUsername(currentFilteredUser.firstName); // Update username if different
              setCurrentUser(currentFilteredUser);
            }
           
          }
          setUserList(filteredUserList);
          setBalance(accResponse.data.balance);
        }
      
        userFetcher();
      }, []); // Dependencies to trigger effect
       //check for the dependency to add if required
    return <div className="ml-4 mt-2  w-screen h-screen flex flex-col ">
        <div id="TopBar" className="flex justify-between mb-5">
            <div id="Title" className="text-4xl font-semibold">Payments App</div>
            <div id="User" className="mr-8 flex justify-around">
                <div id="text">Hello User</div>
                <div className="border-2 border-black ml-2 rounded-full w-10 text-center pt-1" id="logo">{username[0]}</div>
            </div>
        </div>
        <div id="balance" className="flex  mt-5 font-semibold mb-5 ">
            <div id="balText">Your balance  :</div>
            <div className="ml-1"> ₹{balance}</div>
        </div>
        <div id="user" className="mt-5 flex flex-col">
            <div id="userText" className=" text-2xl font-md mb-5">Users</div>
            <div id="userSearchBar" className="w-full flex mb-5">
                <input placeholder="search users..." className="mt-1 ml-4 w-11/12 h-8 border-2 rounded pl-2 font-sm border-gray-500"></input>
            </div>
            <div id="userList" className="flex flex-col  justify-around">
                {
                    userList.map((element) => (
                        <User name={element.firstName} key={element._id} id={element._id} onPress={()=>{
                            naviagate(`/sendMoney?id=${element._id}._id&name=${element.firstName}`)
                        }} />
                    ))
                }
            </div>
        </div>
    </div>
}