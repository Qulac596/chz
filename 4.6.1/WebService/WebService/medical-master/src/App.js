import React from 'react'
import { BrowserRouter } from 'react-router-dom'
// Hooks
import { useAuth } from './hooks/auth.hook'
// Context
import { AuthContext } from './context/AuthContext'
// Routes
import { useRoutes } from './routes'
// Components
import Loader from './components/Loader/Loader'

function App() {
   const { login, logout, token, userId, ready } = useAuth()
   const isAuth = !!token
   const routes = useRoutes(isAuth)

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
         <BrowserRouter>{routes}</BrowserRouter>
      </AuthContext.Provider>
   )
}

export default App
