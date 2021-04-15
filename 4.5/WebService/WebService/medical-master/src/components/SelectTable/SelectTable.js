import React from 'react'
import './SelectTable.scss'
// Components
import BackDrop from '../BackDrop/BackDrop'

// Выпадающий список
function SelectTable({ itemName, data, state, onClick, changeValue, value }) {
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
      <div
         className={
            state
               ? 'select select-table-active active'
               : 'select select-table-active'
         }
         onClick={onClick}
      >
         <div className="list-select-selector table-select">
            <span className="select__current">{itemName}</span>
         </div>

         {state ? (
            <div className="select__body select__table">{items}</div>
         ) : null}

         {state ? <BackDrop onClick={onClick} transparent /> : null}
      </div>
   )
}

export default SelectTable
