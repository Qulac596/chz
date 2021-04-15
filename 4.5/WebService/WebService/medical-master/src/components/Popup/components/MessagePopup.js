import React from 'react'

function MessagePopup({ text, onClick }) {
   return (
      <div>
         <div className="message__title">Сообщение</div>
         <div className="message__text">{text}</div>
         <div className="message__close" onClick={onClick}>
            <div className="message__close-item"></div>
            <div className="message__close-item"></div>
         </div>
         <button className="message__btn btn" onClick={onClick}>
            Продолжить
         </button>
      </div>
   )
}

export default MessagePopup
