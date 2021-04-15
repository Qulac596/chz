import React from 'react'

// Инпут
function Input({ label, type, onChange, placeholder, autoFocus }) {
   return (
      <div>
         <div className="authoriz__text">{label}</div>
         <div className="authoriz__input">
            <input
               type={type}
               onChange={onChange}
               required
               placeholder={placeholder}
               autoFocus={autoFocus}
            />
         </div>
      </div>
   )
}

export default Input
