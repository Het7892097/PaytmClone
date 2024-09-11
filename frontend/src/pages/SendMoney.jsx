import { useSearchParams } from "react-router-dom"
import axios from 'axios';
import { useState } from "react";
export default function SendMoney() {

    const [amount,setAmount]=useState(0);
    const [transfer,setTransfer]=useState(false);
    const [searchParams]=useSearchParams();
    const [success,setSuccess]=useState(false);
    let receiverId=searchParams.get('id');
    console.log(receiverId);
    receiverId=receiverId.substring(0,receiverId.length-4);
    console.log(receiverId);
    let firstName=searchParams.get('name');
    // console.log(id+" "+firstName);
    if(!transfer)
    {return <div id='smParentDiv' className="w-screen bg-gray-300 h-screen border-4 border-black flex justify-center items-center">
        <div id="smMainDiv" className=" flex  flex-col  border bg-white border-red-600 md:w-1/4 md:h-2/6 h-2/5">
            <div id="smTitle" className="mt-10 text-center text-4xl font-semibold">Send Money</div>
            <div id="smBody" className='mt-24 sm:mt-30 pl-4 flex flex-col align-middle w-full'>
                <div id="smUsername" className="flex ">
                    <div className="flex mr-3 justify-center bg-green-500 rounded-full w-8 h-8 text-center">{firstName[0].toUpperCase()}</div>
                    <div className="text-lg font-semibold">{firstName}</div>
                </div>
                <div id="smDisclaimer" className="  font-light ">Amount in ₹(Rs)</div>
                <div className="flex justify-center"><input id="smInput" className="h-6 border w-3/4 border-black rounded mb-8" type='number' onChange={e => setAmount(e.target.value)} ></input></div>
                <div className="flex justify-center"><button className="bg-green-700 text-white rounded w-32 h-8" onClick={async ()=>{
                       const response = await axios.post(
                        'http://localhost:3000/api/v1/account/transfer',
                        {
                          to: receiverId,
                          amount: amount,
                        },
                        {
                          headers: {
                            authorization: localStorage.getItem('authorization'),
                          },
                          maxContentLength: Infinity,  // No size limit for content length
                          maxBodyLength: Infinity      // No size limit for request body length
                        }
                      );
                      
                        if (response.status === 200 || response.status === 201) {
                            setSuccess(true);
                            console.log('Successfully transferred money');
                            console.log(response.data.message);
                        }
                        else{
                            console.log("Unsuccessful transferring of money");
                        }
                    setTransfer(true);
                }}>Transfer Money</button>
                </div>
            </div>
        </div>
    </div>}
    else{
        if(!success){
            return <div>
                <h1>Unsuccessful trasnfer of money</h1>
            </div>
        }
        else{
            return <div>
                <h1>Successfully trasferred money</h1>
            </div>
        }
    }
}