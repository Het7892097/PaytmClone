import { Link } from "react-router-dom"

export function BottomShifter({label,linkContext,to}){
  return <div className="flex">
    <div>{label}</div>
    <Link  className='ml-2 underline hover:text-red-900 ' to={to}>{linkContext}</Link>
  </div>
}