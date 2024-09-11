export default function User({name,id,onPress}){
    return <div className="w-full mt-2  items-center flex justify-between border border-gray rounded-lg pt-2 h-1/4 ">
        <div id="name" className="flex">
            <div className="border border-black rounded-full w-10 mr-2 h-10 pt-1 text-center">{name.charAt(0).toUpperCase()}</div>
            <div id="userName" className="font-semibold mt-1">{name}</div>
        </div>
        <div id="sendMoney">
        <button className="bg-gray-800 border rounded-md h-10 mr-10 w-28 text-white" onClick={onPress}>send-money</button>
        </div>
    </div>
}