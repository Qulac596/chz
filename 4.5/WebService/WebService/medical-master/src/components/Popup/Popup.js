import React from 'react'
import './Popup.scss'
// Components
import ErrorPopup from './components/ErrorPopup'
import BackDrop from '../BackDrop/BackDrop'
import SuccessPopup from './components/SuccessPopup'
import MessagePopup from './components/MessagePopup'
import WarningPopup from './components/WarningPopup'
import ActionPopup from './components/ActionPopup'

// Попап
function Popup({
   error,
   success,
   message,
   warning,
   action,
   text,
   active,
   toggleActive,
}) {
   return (
      <>
         {active ? (
            <div className="popup">
               {error ? (
                  <ErrorPopup text={text} onClick={toggleActive} />
               ) : null}
               {success ? (
                  <SuccessPopup text={text} onClick={toggleActive} />
               ) : null}
               {message ? (
                  <MessagePopup text={text} onClick={toggleActive} />
               ) : null}
               {warning ? (
                  <WarningPopup text={text} onClick={toggleActive} />
               ) : null}
               {action ? (
                  <ActionPopup text={text} onClick={toggleActive} />
               ) : null}
            </div>
         ) : null}

         {active ? <BackDrop onClick={toggleActive} /> : null}
      </>
   )
}

export default Popup
