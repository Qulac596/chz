import React from 'react'
import './Select.scss'
// Components
import BackDrop from '../BackDrop/BackDrop'

// Выпадающий список
function Select({ itemName, data, state, onClick, changeValue, value }) {
   const renderItems = (arr) => {
      return arr.map((item) => (
         <div
            className="list-select-option"
            key={Math.random()}
            onClick={() => changeValue(item)}
         >
            {item[value]}
         </div>
      ))
   }

   const items = renderItems(data)

   return (
      <div className={state ? 'select active' : 'select'} onClick={onClick}>
         <div className="list-select-selector">
            <span className="select__current">{itemName}</span>
         </div>

         {state ? (
            <>
               <div className="select__body-bg"></div>
               <div className="select__body">{items}</div>
            </>
         ) : null}

         {state ? <BackDrop onClick={onClick} transparent /> : null}
      </div>
   )
}

export default Select
