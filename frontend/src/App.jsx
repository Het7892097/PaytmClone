import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { BrowserRouter,Router,Route, Routes } from 'react-router-dom/dist'
import Signup from './pages/Signup'
import Signin from './pages/Signin'
import DashBoard from './pages/DashBoard'
import SendMoney from './pages/SendMoney'

function App() {
  const [count, setCount] = useState(0)

  return (
  <BrowserRouter>
  <Routes>
    <Route path='/signup' element={<Signup/>} />
    <Route path='/signin' element={<Signin/>} />
    <Route path='/dashboard' element={<DashBoard/>} />
    <Route path='/sendMoney' element={<SendMoney/>}/>
  </Routes>
  </BrowserRouter>
  )
}

export default App;
