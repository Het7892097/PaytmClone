import jwt, { decode } from 'jsonwebtoken';

// const axios=require('axios');
const current_user_id='66c353f68629ed01f31a5b3a';
// const codedValue=jwt.sign(current_user_id,'NotYours');
// console.log(codedValue);
// const decoded=jwt.decode(codedValue);
// console.log(decoded);
import axios from 'axios';
var userList=[];
var filteredUserList=[]
axios.get('http://localhost:3000/api/v1/user/bulk')
.then((response)=>{
    console.log(response.data.user);
    userList=response.data.user;
    const tempArr=currentUserFilterer(userList,current_user_id);
    console.log(tempArr[1]);
    // console.log('This is user list:')
    // // console.log(userList);
    // filteredUserList=userList.filter(currentUserFilterer);
    // console.log('This is filtered user list:')
    // console.log(filteredUserList.includes())
  
})

function currentUserFilterer(userList,current_user_id){
    console.log('Inside userFilterer function');
    let userIndex=-1;
   for(let i=0;i<userList.length;++i){
    // console.log(i);
    if(userList[i]._id==current_user_id)
    {
        userIndex=i;
        console.log('Index of the cuurent user in userList is:'+userIndex);
        break;
    }
   }
   //junior developer
//    const currentUser=userList[userIndex];
//Intermediate developer, as the splice array returns the element removed using it
const currentUser= userList.splice(userIndex,1);
//    console.log(userList);
//   const filteredUserList=userList;
return [currentUser,userList];
//   console.log('Filtered user list');
    //  console.log(filteredUserList);
    //  console.log('Cuurent user');
//    console.log(currentUser);
}