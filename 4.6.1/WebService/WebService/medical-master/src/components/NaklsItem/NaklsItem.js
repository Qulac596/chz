import React from 'react'
import './NaklsItem.scss'

function NaklsItem({ item, onClick }) {
   const { status, name, code_count, count, validation, price, nds, sum } = item

   return (
      <tr
         className="table__block-table-coincide"
         onDoubleClick={() => onClick(item)}
      >
         <td>
            <span>{status}</span>
         </td>
         <td>{name}</td>
         <td>{code_count}</td>
         <td>{count}</td>
         <td>{validation}</td>
         <td>{price}</td>
         <td>{nds}</td>
         <td>{sum}</td>
      </tr>
   )
}

export default NaklsItem
