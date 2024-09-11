export default function Inputer({label,textHolder,onChange}){
    return <div className="text-sm">
    <div className="text-left">{label}:</div>
    <input onChange={onChange} className="w-4/6 h-6 mt-2 border-2 rounded border-black" placeholder={textHolder} />
    </div>
}