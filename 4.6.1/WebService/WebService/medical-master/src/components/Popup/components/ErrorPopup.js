import React from 'react'

function ErrorPopup({ text, onClick }) {
   return (
      <div>
         <div className="error__title">ошибка</div>
         <div className="error__text">{text}</div>
         <div className="error__close" onClick={onClick}>
            <div className="error__close-item"></div>
            <div className="error__close-item"></div>
         </div>
         <button className="error__btn btn" onClick={onClick}>
            Продолжить
         </button>
      </div>
   )
}

export default ErrorPopup
