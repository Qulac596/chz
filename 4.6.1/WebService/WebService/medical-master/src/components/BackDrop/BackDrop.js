import React from 'react'
import './BackDrop.scss'

function BackDrop({ onClick, transparent }) {
   return (
      <div
         className={transparent ? 'backDropTransparent' : 'backDrop'}
         onClick={onClick}
      />
   )
}

export default BackDrop
