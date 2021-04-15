import React from 'react'

function SkanItem({ item }) {
   const { sgtin, status } = item

   return (
      <tr className="table__block-table-text">
         <td>1</td>
         <td>{status}</td>
         <td>{sgtin}</td>
      </tr>
   )
}

export default SkanItem
