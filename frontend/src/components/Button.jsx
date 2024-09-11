export default function Button({label,onPress}){
    return <div className="flex w-full justify-center">
        <button className="  bg-black-500 text-white border h-10 w-2/4 rounded-md bg-black" onClick={onPress} >{label}</button>
    </div>
}