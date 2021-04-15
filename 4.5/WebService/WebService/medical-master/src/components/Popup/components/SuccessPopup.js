import React from 'react'

function SuccessPopup({ text, onClick }) {
   return (
      <div>
         <div className="successfully__title">успешно</div>
         <div className="successfully__text">{text}</div>
         <div className="successfully__close" onClick={onClick}>
            <div className="successfully__close-item"></div>
            <div className="successfully__close-item"></div>
         </div>
         <button className="successfully__btn btn" onClick={onClick}>
            Продолжить
         </button>
      </div>
   )
}

export default SuccessPopup
