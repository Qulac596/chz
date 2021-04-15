import React from 'react'

function WarningPopup({ text, onClick }) {
   return (
      <div>
         <div className="warning__title">ошибка</div>
         <div className="warning__text">{text}</div>
         <div className="warning__close" onClick={onClick}>
            <div className="warning__close-item"></div>
            <div className="warning__close-item"></div>
         </div>
         <button className="warning__btn btn" onClick={onClick}>
            Продолжить
         </button>
      </div>
   )
}

export default WarningPopup
