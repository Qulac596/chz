import React from 'react'
// Components
import Header from '../components/Header/Header'

// Обертка для страниц
function Layout({ children }) {
   return (
      <div className="container">
         <Header />
         {children}
      </div>
   )
}

export default Layout
