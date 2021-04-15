import React, { useContext, useState, useEffect, useMemo } from 'react'
import axios from '../axios'
import { Link, useHistory } from 'react-router-dom'
import {
   useTable,
   useResizeColumns,
   useSortBy,
   useFlexLayout,
   useRowSelect,
} from 'react-table'
// Context
import { AuthContext } from '../context/AuthContext'
import { MainFilterContext } from '../context/MainFilterContext'
// Components
import Loader from '../components/Loader/Loader'
import Popup from '../components/Popup/Popup'
import Select from '../components/Select/Select'
// Utils
import { formatDate, isEmptyObject } from '../utils'
// HOC
import Layout from '../hoc/Layout'

// Страница главная
function MainPage() {
   const history = useHistory()
   const auth = useContext(AuthContext)
   const [data, setData] = useState([])
   const [loading, setLoading] = useState(true)
   const [error, setError] = useState(false)

   const defaultColumn = useMemo(
      () => ({
         minWidth: 30,
         width: 150,
         maxWidth: 200,
      }),
      []
   )

   const columns = useMemo(
      () => [
         {
            Header: 'Статус',
            accessor: 'status',
         },
         {
            Header: '№',
            accessor: 'doc_num',
         },
         {
            Header: 'Дата',
            accessor: 'doc_date',
            Cell: (cellObj) => formatDate(cellObj.row.original.doc_date),
         },
         {
            Header: 'Поставщик',
            accessor: 'provider',
         },
         {
            Header: 'Акцент',
            accessor: 'acceptance_type',
         },
         {
            Header: 'Тип договора',
            accessor: 'contract_type',
         },
         {
            Header: 'Сумма, Р.',
            accessor: 'sum',
         },
      ],
      []
   )

   const { getTableProps, headerGroups, rows, prepareRow } = useTable(
      {
         columns,
         data,
         defaultColumn,
      },
      useSortBy,
      useResizeColumns,
      useFlexLayout,
      useRowSelect
   )

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

   const [statuses, setStatuses] = useState([])
   const [companies, setCompanies] = useState([])

   const {
      year: [year, setYear],
      month: [month, setMonth],
      status: [status, setStatus],
      company: [company, setCompany],
      years,
      months,
   } = useContext(MainFilterContext)

   useEffect(() => {
      setLoading(true)

      const params = {
         company_id: company.company_id,
         year: year.value,
         month: month.value,
         status_id: status.nakl_status_id,
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
      if (status.nakl_status_id === 'all') {
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
   }, [
      year.value,
      month.value,
      status.nakl_status_id,
      company.company_id,
      auth.token,
   ])

   useEffect(() => {
      setLoading(true)

      async function fetchData() {
         try {
            const responseCompanies = await axios.get('/companies', {
               headers: { accessToken: auth.token },
            })
            const responseStatuses = await axios.get(
               '/reference-books/nakl-statuses',
               {
                  headers: { accessToken: auth.token },
               }
            )

            if (
               responseCompanies.data.is_error ||
               responseStatuses.data.is_error
            ) {
               setError(true)
            } else {
               responseCompanies.data.data.push({ name: 'Все', value: 'all' })
               responseStatuses.data.data.push({
                  nakl_status_id: 'all',
                  value: 'Все',
               })
               setCompanies(responseCompanies.data.data)
               setStatuses(responseStatuses.data.data)
               if (isEmptyObject(status) && isEmptyObject(company)) {
                  setCompany(
                     responseCompanies.data.data[
                        responseCompanies.data.data.length - 1
                     ]
                  )
                  setStatus(
                     responseStatuses.data.data[
                        responseStatuses.data.data.length - 1
                     ]
                  )
               }
               setLoading(false)
            }
         } catch (e) {
            setLoading(false)
            setError(true)
         }
      }
      fetchData()
   }, [auth.token])

   const clickHandler = (id) => {
      history.push(`/nakl-one/${id}`)
   }

   const redirectUrlHandler = () => {
      auth.logout()
   }

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
                           value="name"
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
                           value="name"
                        />
                     </div>

                     <div className="list-select">
                        <div className="list-select-text">Статус</div>
                        <Select
                           onClick={toggleStatusSelectActive}
                           data={statuses}
                           state={statusSelectActive}
                           changeValue={setStatus}
                           itemName={status.value}
                           value="value"
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
                           value="name"
                        />
                     </div>
                  </div>
                  <div className="table">
                     <div className="table__block-wrapper">
                        <div {...getTableProps()}>
                           <div>
                              {headerGroups.map((headerGroup) => (
                                 <div
                                    {...headerGroup.getHeaderGroupProps()}
                                    className="tr"
                                 >
                                    {headerGroup.headers.map((column) => (
                                       <div
                                          {...column.getHeaderProps(
                                             column.getSortByToggleProps()
                                          )}
                                          className="th"
                                       >
                                          {column.render('Header')}
                                          {column.canResize && (
                                             <div
                                                {...column.getResizerProps()}
                                                className={`resizer ${
                                                   column.isResizing
                                                      ? 'isResizing'
                                                      : ''
                                                }`}
                                             />
                                          )}
                                       </div>
                                    ))}
                                 </div>
                              ))}
                           </div>
                           <div className="tbody">
                              {rows.map((row) => {
                                 prepareRow(row)
                                 return (
                                    <div
                                       {...row.getRowProps()}
                                       className={row.original.status_style}
                                       onDoubleClick={() =>
                                          clickHandler(row.original.nakl_id)
                                       }
                                    >
                                       {row.cells.map((cell) => {
                                          return (
                                             <div
                                                {...cell.getCellProps()}
                                                className="td"
                                             >
                                                {cell.render('Cell')}
                                             </div>
                                          )
                                       })}
                                    </div>
                                 )
                              })}
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </Layout>
   )
}

export default MainPage
