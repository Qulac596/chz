import React from 'react'
import './Input.scss'

function Input({
   label,
   type,
   onChange,
   placeholder,
   disabled,
   innSearch,
   innNotFound,
}) {
   return (
      <div className="edit__block-form-item">
         <div className="edit__block-form-left">{label}</div>
         <div className="edit__block-form-right">
            <input type={type} onChange={onChange} placeholder={placeholder} />

            {innSearch ? (
               <div className="infoText">Идет поиск данных.</div>
            ) : null}
            {innNotFound ? (
               <div className="infoText">Поставщик с таким ИНН не найден.</div>
            ) : null}
         </div>

         {disabled ? <div className="bg-input-disabled"></div> : null}
      </div>
   )
}

export default Input
