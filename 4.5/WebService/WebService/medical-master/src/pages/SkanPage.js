import React, { useContext, useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import axios from '../axios'
// Components
import Loader from '../components/Loader/Loader'
import Popup from '../components/Popup/Popup'
import SkanItem from '../components/SkanItem/SkanItem'
// Context
import { AuthContext } from '../context/AuthContext'
// HOC
import Layout from '../hoc/Layout'

// Страница штрих кодов
function SkanPage() {
   const [data, setData] = useState(null)
   const [loading, setLoading] = useState(true)
   const [error, setError] = useState(false)
   const auth = useContext(AuthContext)
   const linkId = useParams().id
   const naklId = useParams().naklId
   const [errorStringSuccess, setErrorStringSuccess] = useState(false)
   const [errorString, setErrorString] = useState('')

   const [term, setTerm] = useState('')

   const onChangeHandler = async (e) => {
      setError(false)
      setErrorStringSuccess(false)
      setTerm(e.target.value)

      try {
         const response = await axios.post(
            `/sgtins/${linkId}`,
            [e.target.value],
            {
               headers: { accessToken: auth.token },
            }
         )

         if (response.data.is_error) {
            setErrorStringSuccess(true)
            setErrorString(response.data.error_message)
         } else {
            const dataCopy = [...data]
            console.log(response.data.data)
            setData(dataCopy.concat(response.data.data))
         }
      } catch (e) {
         setLoading(false)
         setError(true)
      }
   }

   useEffect(() => {
      setError(false)
      setLoading(true)

      async function fetchData() {
         try {
            const response = await axios.get(`/sgtins/${linkId}`, {
               headers: { accessToken: auth.token },
            })

            if (response.data.is_error) {
               setError(true)
            } else {
               setData(response.data.data)
            }

            setLoading(false)
         } catch (e) {
            setLoading(false)
            setError(true)
         }
      }
      fetchData()
   }, [linkId, naklId, auth.token])

   const renderItems = (arr) => {
      return arr.map((item) => <SkanItem key={Math.random()} item={item} />)
   }

   const redirectUrlHandler = () => auth.logout()

   if (loading) {
      return <Loader />
   }

   if (error) {
      return (
         <Popup
            active={error}
            error
            text="Что-то пошло не так, попробуйте позже!"
            toggleActive={redirectUrlHandler}
         />
      )
   }

   const items = renderItems(data)

   return (
      <div className="skan">
         <Layout>
            <div className="skan__block">
               <div className="skan__block-title">
                  Сканирование кодов маркировки
               </div>
               <div className="skan__block-subtitle">
                  Ацц 600МГ порошок д/приг.Р-ра/приемка внутрь пак. X6 (R)
               </div>

               <div className="skan__block-wp">
                  <div className="skan__block-img">
                     <img src="../images/skan.png" alt="" />
                  </div>
                  <div className="skan__block-num">
                     <div className="skan__block-number">
                        {data.length || 0}
                     </div>
                     <div className="skan__block-text">
                        Отсканирование кодов
                     </div>
                  </div>
               </div>

               <div className="table__block-wrapper">
                  <table className="table__block-table">
                     <thead>
                        <tr className="table__block-title">
                           <th>№</th>
                           <th colSpan="2">Коды маркировки</th>
                        </tr>
                     </thead>
                     <tbody>{items}</tbody>
                  </table>
               </div>

               <form className="hidden_input">
                  <input
                     type="text"
                     onChange={onChangeHandler}
                     autoFocus={true}
                     onBlur={({ target }) => target.focus()}
                     value={term}
                  />
               </form>

               <div className="skan__button">
                  <Link to={'/nakl-one/' + naklId}>
                     <div className="btn skan__button-btn">Продолжить</div>
                  </Link>
                  {errorStringSuccess ? (
                     <div className="skan_error">{errorString}</div>
                  ) : null}
               </div>
            </div>
         </Layout>
      </div>
   )
}

export default SkanPage
