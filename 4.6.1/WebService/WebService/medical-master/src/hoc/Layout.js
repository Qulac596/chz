import React from 'react'
// Components
import Header from '../components/Header/Header'

function Layout({ children }) {
   return (
      <div className="container">
         <Header />
         {children}
      </div>
   )
}

export default Layout
