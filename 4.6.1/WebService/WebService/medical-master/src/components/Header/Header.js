import React, { useContext } from 'react'
import { Link } from 'react-router-dom'
import './Header.scss'
// Context
import { AuthContext } from '../../context/AuthContext'

function Header() {
   const auth = useContext(AuthContext)

   const linkToLastNakl = auth.naklId ? '/nakl-one/' + auth.naklId : '/'

   return (
      <header className="header">
         <ul className="menu">
            <Link to="/">
               <li className="menu__list">
                  <div className="menu__list-link">Справочники</div>
               </li>
            </Link>

            <Link to="/">
               <li className="menu__list">
                  <div className="menu__list-link">Накладные</div>
               </li>
            </Link>
            <Link to={linkToLastNakl}>
               <li className="menu__list">
                  <div className="menu__list-link">Накладная</div>
               </li>
            </Link>
            <li className="menu__list">
               <div className="menu__list-link">
                  {auth.userId}
                  <div className="menu__list-sublink">
                     <div
                        className="menu__list-sublink-word"
                        onClick={auth.logout}
                     >
                        <span>Выйти</span>
                        {/* <div className="menu__list-sublink-block">
                           <div className="menu__list-sublink-subword">
                              Справочники
                           </div>
                           <div className="menu__list-sublink-subword">
                              Справочники
                           </div>
                           <div className="menu__list-sublink-subword">
                              Справочники
                           </div>
                           <div className="menu__list-sublink-subword">
                              Справочники
                           </div>
                        </div> */}
                     </div>
                  </div>
               </div>
            </li>
         </ul>
      </header>
   )
}

export default Header
