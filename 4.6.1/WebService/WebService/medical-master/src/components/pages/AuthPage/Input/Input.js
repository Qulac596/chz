import React from 'react'

function Input({ label, type, onChange, placeholder }) {
   return (
      <div>
         <div className="authoriz__text">{label}</div>
         <div className="authoriz__input">
            <input
               type={type}
               onChange={onChange}
               required
               placeholder={placeholder}
            />
         </div>
      </div>
   )
}

export default Input
