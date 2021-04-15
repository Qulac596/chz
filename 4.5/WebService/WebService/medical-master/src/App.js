import React, { useState } from 'react'
import { BrowserRouter } from 'react-router-dom'
// Hooks
import { useAuth } from './hooks/auth.hook'
// Context
import { AuthContext } from './context/AuthContext'
// Routes
import { useRoutes } from './routes'
// Components
import Loader from './components/Loader/Loader'
import { MainFilterContext } from './context/MainFilterContext'
// Data
import { months, years } from './data'

// Инициализация приложения
function App() {
   const { login, logout, token, userId, ready } = useAuth()
   const isAuth = !!token
   const routes = useRoutes(isAuth)

   // Main Filter
   const [year, setYear] = useState({
      name: new Date().getFullYear(),
      value: new Date().getFullYear(),
   })
   const [month, setMonth] = useState({
      name: months[new Date().getMonth()].name,
      value: new Date().getMonth() + 1,
   })
   const [status, setStatus] = useState({})
   const [company, setCompany] = useState({})

   if (!ready) {
      return <Loader />
   }

   return (
      <AuthContext.Provider
         value={{
            login,
            logout,
            token,
            userId,
            isAuth,
         }}
      >
         <MainFilterContext.Provider
            value={{
               year: [year, setYear],
               month: [month, setMonth],
               status: [status, setStatus],
               company: [company, setCompany],
               years,
               months,
            }}
         >
            <BrowserRouter>{routes}</BrowserRouter>
         </MainFilterContext.Provider>
      </AuthContext.Provider>
   )
}

export default App
