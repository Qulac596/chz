import React from 'react'
import './FormSelect.scss'
// Components
import BackDrop from '../BackDrop/BackDrop'

// Форма
function FormSelect({
   itemName,
   label,
   data,
   state,
   onClick,
   changeValue,
   item,
   disabled,
}) {
   const renderItems = (arr) => {
      return arr.map((item) => (
         <div
            className="edit__block-option"
            key={Math.random()}
            onClick={() => changeValue(item)}
         >
            {item[itemName] || ''}
         </div>
      ))
   }

   const items = renderItems(data)

   return (
      <div
         className="edit__block-form-item"
         onClick={!disabled ? onClick : null}
      >
         <div className="edit__block-form-left">{label}</div>
         <div
            className={
               state
                  ? 'select edit__block-form-right active'
                  : 'select edit__block-form-right'
            }
         >
            <div className="edit__block-selector edit__block-form-right">
               <span className="select__current">{item[itemName] || ''}</span>
            </div>
            {state ? (
               <>
                  <div className="select__body-bg"></div>
                  <div className="select__body">{items}</div>
               </>
            ) : null}

            {state ? <BackDrop onClick={onClick} transparent /> : null}
         </div>

         {disabled ? <div className="bg-disabled"></div> : null}
      </div>
   )
}

export default FormSelect
