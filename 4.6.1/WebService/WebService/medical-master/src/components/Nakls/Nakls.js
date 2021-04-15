import React from 'react'
import { formatDate } from '../../utils'

function Nakls({ item, onClick }) {
   const {
      status,
      doc_num,
      doc_date,
      provider,
      acceptance_type,
      contract_type,
      sum,
      status_style,
   } = item

   const formattedDate = formatDate(doc_date)

   return (
      <tr className={status_style} onDoubleClick={onClick}>
         <td>
            <span>{status}</span>
         </td>
         <td>{doc_num}</td>
         <td>{formattedDate}</td>
         <td>{provider}</td>
         <td>{acceptance_type}</td>
         <td>{contract_type}</td>
         <td>{sum}</td>
      </tr>
   )
}

export default Nakls
