import React, { useEffect, useState, useContext } from 'react'
import axios from '../../axios'
import './NaklsItem.scss'
// Context
import { AuthContext } from '../../context/AuthContext'
// Components
import SelectTable from '../SelectTable/SelectTable'

// Строка таблицы для одной накладной
function NaklsItem({ item, onClick }) {
   const { status, name, code_count, count, validation, price, nds, sum } = item
   const auth = useContext(AuthContext)
   const [term, setTerm] = useState(price)
   const [data, setData] = useState([])
   const [selectActive, setSelectActive] = useState(false)
   const [ndsValue, setNdsValue] = useState({ value: nds, is_default: false })

   const toggleSelectActive = () => {
      setSelectActive(!selectActive)
   }

   const onChange = (e) => {
      setTerm(e.target.value)
   }

   useEffect(() => {
      async function fetchData() {
         try {
            const response = await axios.get('/reference-books/nds-values', {
               headers: { accessToken: auth.token },
            })
            response.data.data.push({ value: 'Без НДС', is_default: false })
            setData(response.data.data)
         } catch (e) {
            console.log(e)
         }
      }
      fetchData()
   }, [auth])

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
         <td>
            <input
               className="nakl-input"
               onChange={onChange}
               type="text"
               value={term}
            />
         </td>
         <td className="list-select list-select-item">
            <SelectTable
               onClick={toggleSelectActive}
               data={data}
               state={selectActive}
               changeValue={setNdsValue}
               itemName={ndsValue.value}
               value="value"
            />
         </td>
         <td>{sum}</td>
      </tr>
   )
}

export default NaklsItem
