import React, { useContext, useEffect, useState } from 'react'
import { Link, useParams, useHistory } from 'react-router-dom'
import axios from '../axios'
// Components
import Loader from '../components/Loader/Loader'
import NaklsItem from '../components/NaklsItem/NaklsItem'
import Popup from '../components/Popup/Popup'
import SelectData from '../components/SelectDate/SelectDate'
// Context
import { AuthContext } from '../context/AuthContext'
// HOC
import Layout from '../hoc/Layout'
// Utils
import { formatDate } from '../utils'

function NaklOnePage() {
   const history = useHistory()
   const linkId = useParams().id
   const [data, setData] = useState({})
   const [dataItems, setDataItems] = useState([])
   const [dataStatuses, setDataStatuses] = useState([])
   const [loading, setLoading] = useState(true)
   const [error, setError] = useState(false)
   const auth = useContext(AuthContext)

   const [dateOfAccept, setDateOfAccept] = useState(new Date())

   const removeItem = async () => {
      try {
         await axios.delete(`/nakls-full/-${linkId}`, {
            headers: { accessToken: auth.token },
         })

         history.push('/')
      } catch (e) {
         setError(true)
      }
   }

   const successItem = async () => {
      try {
         await axios.put(`/nakls/sign-and-send/${linkId}`, null, {
            headers: { accessToken: auth.token },
         })
      } catch (e) {
         setError(true)
      }
   }

   const receptionItem = async () => {
      try {
         await axios.put(`/nakls/trust-accept/${linkId}`, null, {
            headers: { accessToken: auth.token },
         })
      } catch (e) {
         setError(true)
      }
   }

   useEffect(() => {
      setError(false)

      async function fetchData() {
         try {
            const response = await axios.get(`/nakls/${linkId}`, {
               headers: { accessToken: auth.token },
            })
            const responseItems = await axios.get(`/nakl-items/${linkId}`, {
               headers: { accessToken: auth.token },
            })
            const responseStatuses = await axios.get(
               '/reference-books/nakl-statuses',
               {
                  headers: { accessToken: auth.token },
               }
            )

            auth.naklId = linkId

            if (
               response.data.is_error ||
               responseItems.data.is_error ||
               responseStatuses.data.is_error
            ) {
               setError(true)
            } else {
               setDateOfAccept(new Date(response.data.data.doc_date))
               setDataItems(responseItems.data.data)
               setData(response.data.data)
               setDataStatuses(responseStatuses.data.data)
            }

            setLoading(false)
         } catch (e) {
            setLoading(false)
            setError(true)
         }
      }
      fetchData()
   }, [linkId, auth.token, auth])

   const clickHandler = (item) => {
      history.push(`/nakl-one/${linkId}/skan/${item.nakl_item_id}`)
   }

   const editNaklPageRedirect = () => {
      history.push({
         pathname: '/edit',
         state: { data },
      })
   }

   const renderItems = (arr) => {
      return arr.map((item) => (
         <NaklsItem item={item} key={Math.random()} onClick={clickHandler} />
      ))
   }

   const renderStatuses = (arr) => {
      return arr.map((item) => (
         <React.Fragment key={item.nakl_status_id}>
            {item.nakl_status_id !== 5 ? (
               <div
                  className={
                     data.nakl_status_id === item.nakl_status_id
                        ? `accent__block-post-point${item.nakl_status_id} point active`
                        : data.nakl_status_id >= item.nakl_status_id
                        ? `accent__block-post-point${item.nakl_status_id} point done`
                        : `accent__block-post-point${item.nakl_status_id} point`
                  }
               >
                  <div
                     className={`accent__block-post-point${item.nakl_status_id}-text`}
                  >
                     {item.value}
                  </div>
               </div>
            ) : null}
         </React.Fragment>
      ))
   }

   const redirectUrlHandler = () => auth.logout()

   if (loading) {
      return <Loader />
   }

   if (error) {
      return (
         <Popup
            active={true}
            error
            text="Что-то пошло не так, попробуйте позже!"
            toggleActive={redirectUrlHandler}
         />
      )
   }

   const {
      acceptance_type,
      doc_num,
      doc_date,
      provider_address,
      nakl_status_id,
      provider_inn,
      provider_name,
      receiver_name,
      receiver_inn,
      receiver_address,
   } = data

   const content = renderItems(dataItems)
   const statuses = renderStatuses(dataStatuses)

   const formattedDate = formatDate(doc_date)
   const currentStatus = dataStatuses.find(
      (status) => status.nakl_status_id === nakl_status_id
   ).value

   return (
      <Layout>
         <div className="accent__block">
            <div className="accent__block-title" onClick={editNaklPageRedirect}>
               {acceptance_type} акцепт №{doc_num} от {formattedDate}
            </div>
            <div
               className="accent__block-subtitle"
               onClick={editNaklPageRedirect}
            >
               {currentStatus}
            </div>
            <div className="accent__block-post" onClick={editNaklPageRedirect}>
               <div className="accent__block-post-left">
                  <div className="accent__block-post-title">Поставщик</div>
                  <div className="accent__block-post-subtitle">
                     {provider_name}
                  </div>
                  <div className="accent__block-post-inn">
                     ИНН {provider_inn}
                  </div>
                  <div className="accent__block-post-text">
                     {provider_address}
                  </div>
               </div>
               <div className="accent__block-post-right">
                  <div className="accent__block-post-title">Получатель</div>
                  <div className="accent__block-post-subtitle">
                     {receiver_name}
                  </div>
                  <div className="accent__block-post-inn">
                     ИНН {receiver_inn}
                  </div>
                  <div className="accent__block-post-text">
                     {receiver_address}
                  </div>
               </div>
            </div>
            <div className="accent__block-post-line">{statuses}</div>

            <div className="accent__block-select">
               <div className="list-select">
                  <div className="list-select-text">Дата приемки</div>
                  <SelectData onChange={setDateOfAccept} data={dateOfAccept} />
               </div>
               <div className="list-select">
                  <div className="accent__block-toggle">
                     <div className="accent__block-toggle-point"></div>
                  </div>
                  <div className="list-select-text">Отозванные товары</div>
               </div>
            </div>

            <div className="table__block-wrapper">
               <table className="table__block-table">
                  <thead>
                     <tr className="table__block-title">
                        <th>Статус</th>
                        <th>Наименование</th>
                        <th>Коды маркировки</th>
                        <th>Кол-во</th>
                        <th>Проверка</th>
                        <th>Цена, Р.</th>
                        <th>НДС, Р.</th>
                        <th>Сумма(с НДС)</th>
                     </tr>
                  </thead>
                  <tbody>{content}</tbody>
               </table>
            </div>

            <div className="accent__block-button">
               <Link to="/">
                  <button className="btn">Закрыть</button>
               </Link>

               <button className="btn" onClick={receptionItem}>
                  Доверительная приемка
               </button>
               <button className="btn" onClick={removeItem}>
                  Удалить
               </button>
               <button className="btn" onClick={successItem}>
                  Завершить приемку
               </button>
            </div>
         </div>
      </Layout>
   )
}

export default NaklOnePage
