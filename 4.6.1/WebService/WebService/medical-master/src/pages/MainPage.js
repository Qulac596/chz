import React, { useContext, useState, useEffect } from 'react'
import axios from '../axios'
import { Link, useHistory } from 'react-router-dom'
// Context
import { AuthContext } from '../context/AuthContext'
// Components
import Loader from '../components/Loader/Loader'
import Nakls from '../components/Nakls/Nakls'
import Popup from '../components/Popup/Popup'
import Select from '../components/Select/Select'
// Utils
import { getYears } from '../utils'
// HOC
import Layout from '../hoc/Layout'

function MainPage() {
   const history = useHistory()
   const auth = useContext(AuthContext)
   const [data, setData] = useState([])
   const [loading, setLoading] = useState(true)
   const [error, setError] = useState(false)

   const [yearSelectActive, setYearSelectActive] = useState(false)
   const [monthSelectActive, setMonthSelectActive] = useState(false)
   const [statusSelectActive, setStatusSelectActive] = useState(false)
   const [companySelectActive, setCompanySelectActive] = useState(false)

   const toggleYearSelectActive = () => {
      setYearSelectActive(!yearSelectActive)
      setMonthSelectActive(false)
      setStatusSelectActive(false)
      setCompanySelectActive(false)
   }
   const toggleMonthSelectActive = () => {
      setMonthSelectActive(!monthSelectActive)
      setYearSelectActive(false)
      setStatusSelectActive(false)
      setCompanySelectActive(false)
   }

   const toggleStatusSelectActive = () => {
      setStatusSelectActive(!statusSelectActive)
      setYearSelectActive(false)
      setMonthSelectActive(false)
      setCompanySelectActive(false)
   }

   const toggleCompanySelectActive = () => {
      setCompanySelectActive(!companySelectActive)
      setYearSelectActive(false)
      setMonthSelectActive(false)
      setStatusSelectActive(false)
   }

   const years = getYears(2002)
   years.push({ name: 'Все', value: 'all' })

   const months = [
      { name: 'Январь', value: 1 },
      { name: 'Февраль', value: 2 },
      { name: 'Март', value: 3 },
      { name: 'Апрель', value: 4 },
      { name: 'Май', value: 5 },
      { name: 'Июнь', value: 6 },
      { name: 'Июль', value: 7 },
      { name: 'Август', value: 8 },
      { name: 'Сентябрь', value: 9 },
      { name: 'Октябрь', value: 10 },
      { name: 'Ноябрь', value: 11 },
      { name: 'Декабрь', value: 12 },
      { name: 'Все', value: 'all' },
   ]
   const statuses = [
      { name: 'Завершен', value: 0 },
      { name: 'Не Завершен', value: 1 },
      { name: 'Все', value: 'all' },
   ]
   const [companies, setCompanies] = useState([])

   const [year, setYear] = useState({
      name: new Date().getFullYear(),
      value: new Date().getFullYear(),
   })
   const [month, setMonth] = useState({
      name: months[new Date().getMonth()].name,
      value: new Date().getMonth() + 1,
   })
   const [status, setStatus] = useState({ name: 'Не Завершен', value: 1 })
   const [company, setCompany] = useState({})

   useEffect(() => {
      setLoading(true)

      const params = {
         company_id: company.company_id,
         year: year.value,
         month: month.value,
         status_id: status.value,
      }

      if (company.company_id === 'all') {
         delete params.company_id
      }
      if (year.value === 'all') {
         delete params.year
      }
      if (month.value === 'all') {
         delete params.month
      }
      if (status.value === 'all') {
         delete params.status_id
      }

      async function fetchData() {
         try {
            const response = await axios.get('/nakls/filtr', {
               params,
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
   }, [year.value, month.value, status.value, company.company_id, auth.token])

   useEffect(() => {
      setLoading(true)

      async function fetchData() {
         try {
            const response = await axios.get('/companies', {
               headers: { accessToken: auth.token },
            })

            if (response.data.is_error) {
               setError(true)
            } else {
               response.data.data.push({ name: 'Все', value: 'all' })
               setCompanies(response.data.data)
               setCompany(response.data.data[0])
            }
            setLoading(false)
         } catch (e) {
            setLoading(false)
            setError(true)
         }
      }
      fetchData()
   }, [auth.token])

   const clickHandler = (item) => {
      history.push(`/nakl-one/${item.nakl_id}`)
   }

   const renderItems = (arr) => {
      return arr.map((item) => (
         <Nakls
            item={item}
            key={Math.random()}
            onClick={() => clickHandler(item)}
         />
      ))
   }

   const redirectUrlHandler = () => {
      auth.logout()
   }

   const content = renderItems(data)

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

   return (
      <Layout>
         <div className="overhead">
            <div className="overhead__block">
               <div className="overhead__block-top">
                  <div className="overhead__block-top-right">
                     <div className="overhead__block-top-title">
                        Накладные
                        <Link to="/create">
                           <button className="buttonPlus">
                              <div className="buttonPlus-item"></div>
                              <div className="buttonPlus-item"></div>
                           </button>
                        </Link>
                     </div>
                     <div className="overhead__block-top-subtitle">
                        Поясняющий текст
                     </div>
                  </div>
               </div>
               <div className="overhead__block-bottom">
                  <div className="overhead__block-select">
                     <div className="list-select">
                        <div className="list-select-text">Год</div>
                        <Select
                           onClick={toggleYearSelectActive}
                           data={years}
                           state={yearSelectActive}
                           changeValue={setYear}
                           itemName={year.name}
                        />
                     </div>

                     <div className="list-select">
                        <div className="list-select-text">Месяц</div>
                        <Select
                           onClick={toggleMonthSelectActive}
                           data={months}
                           state={monthSelectActive}
                           changeValue={setMonth}
                           itemName={month.name}
                        />
                     </div>

                     <div className="list-select">
                        <div className="list-select-text">Статус</div>
                        <Select
                           onClick={toggleStatusSelectActive}
                           data={statuses}
                           state={statusSelectActive}
                           changeValue={setStatus}
                           itemName={status.name}
                        />
                     </div>

                     <div className="list-select">
                        <div className="list-select-text">Поставщик</div>
                        <Select
                           onClick={toggleCompanySelectActive}
                           data={companies}
                           state={companySelectActive}
                           changeValue={setCompany}
                           itemName={company.name}
                        />
                     </div>
                  </div>

                  <div className="table__block-wrapper">
                     <table className="table__block-table">
                        <thead>
                           <tr className="table__block-title">
                              <th>Статус</th>
                              <th>№</th>
                              <th>Дата</th>
                              <th>Поставщик</th>
                              <th>Акцент</th>
                              <th>Тип договора</th>
                              <th>Сумма, Р.</th>
                           </tr>
                        </thead>
                        <tbody>{content}</tbody>
                     </table>
                  </div>
               </div>
            </div>
         </div>
      </Layout>
   )
}

export default MainPage
