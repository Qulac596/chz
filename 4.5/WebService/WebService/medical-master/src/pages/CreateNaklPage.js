import React, { useContext, useState, useEffect } from 'react'
import axios from '../axios'
import { useHistory } from 'react-router-dom'
import format from 'date-fns/format'
// Context
import { AuthContext } from '../context/AuthContext'
// Components
import Loader from '../components/Loader/Loader'
import Popup from '../components/Popup/Popup'
import FormSelect from '../components/FormSelect/FormSelect'
import Input from '../components/pages/CreateNaklPage/Input/Input'
import SelectDate from '../components/pages/CreateNaklPage/SelectDate/SelectDate'
// Utils
import { isEmptyObject, validateControl } from '../utils'

// Страница создания накладной
function CreateNaklPage() {
   const [form, setForm] = useState({
      isFormValid: false,
      formControls: {
         INN: {
            value: '',
            type: 'text',
            label: 'ИНН поставщика',
            placeholder: 'ИНН',
            errorMessage: 'Введите корректно ИНН',
            valid: false,
            touched: false,
            validation: {
               required: false,
            },
         },
         invoiceNumber: {
            value: '',
            type: 'text',
            label: 'Номер накладной',
            placeholder: '',
            errorMessage: 'Введите корректно',
            valid: false,
            touched: false,
            validation: {
               required: true,
            },
         },
         contractNumber: {
            value: '',
            type: 'text',
            label: 'Номер контракта',
            placeholder: '',
            errorMessage: 'Введите корректно',
            valid: false,
            touched: false,
            validation: {
               required: false,
            },
         },
      },
   })
   const history = useHistory()
   const auth = useContext(AuthContext)
   const [loading, setLoading] = useState(false)
   const [error, setError] = useState(false)

   const [activePopup, setActivePopup] = useState(false)

   const [addressDisabled, setAddressDisabled] = useState(true)
   const [innSearch, setInnSearch] = useState(false)
   const [innNotFound, setInnNotFound] = useState(false)

   const [companiesData, setCompaniesData] = useState([])
   const [addressData, setAddressData] = useState([])
   const [receiveTypesData, setReceiveTypesData] = useState([])
   const [sourceTypesData, setSourceTypesData] = useState([])
   const [contractTypesData, setContractTypesData] = useState([])
   const [turnoverTypesData, setTurnoverTypesData] = useState([])

   const [selectCompanies, setSelectCompanies] = useState(false)
   const [selectAddress, setSelectAddress] = useState(false)
   const [selectReceiveTypes, setSelectReceiveTypes] = useState(false)
   const [selectSourceTypes, setSelectSourceTypes] = useState(false)
   const [selectContractTypes, setSelectContractTypes] = useState(false)
   const [selectTurnoverTypes, setSelectTurnoverTypes] = useState(false)

   const toggleSelectCompanies = () => setSelectCompanies(!selectCompanies)
   const toggleSelectAddress = () => setSelectAddress(!selectAddress)
   const toggleSelectReceiveTypes = () =>
      setSelectReceiveTypes(!selectReceiveTypes)
   const toggleSelectSourceTypes = () =>
      setSelectSourceTypes(!selectSourceTypes)
   const toggleSelectContractTypes = () =>
      setSelectContractTypes(!selectContractTypes)
   const toggleSelectTurnoverTypes = () =>
      setSelectTurnoverTypes(!selectTurnoverTypes)

   const [company, setCompany] = useState({})
   const [address, setAddress] = useState({})
   const [receiveTypes, setReceiveTypes] = useState({})
   const [sourceTypes, setSourceTypes] = useState({})
   const [contractTypes, setContractTypes] = useState({})
   const [turnoverTypes, setTurnoverTypes] = useState({})
   const [invoiceDate, setInvoiceDate] = useState(new Date())
   const [getProductDate, setGetProductDate] = useState(new Date())

   const setCompanyHandler = (item) => {
      setCompany(item)
      setAddress({})
   }

   const togglePopup = () => setActivePopup(!activePopup)

   const onChangeHandler = (e, item) => {
      const formControls = { ...form.formControls }
      const inputItem = formControls[item]
      inputItem.value = e.target.value
      inputItem.touched = true
      inputItem.valid = validateControl(inputItem.value, inputItem.validation)

      if (item === 'INN') {
         async function fetchData() {
            try {
               const response = await axios.get('/companies', {
                  params: { inn: inputItem.value },
                  headers: { accessToken: auth.token },
               })

               if (response.data.data.status === 'not_found') {
                  setInnNotFound(true)
                  setInnSearch(false)
                  setCompany({})
               } else if (response.data.data.status === 'search') {
                  setInnSearch(true)
                  setInnNotFound(false)
                  setCompany({})
               } else {
                  setCompany(response.data.data.company)
                  setInnSearch(false)
                  setInnNotFound(false)
               }
            } catch (e) {
               setError(true)
            }
         }
         fetchData()
      } else {
         setInnNotFound(false)
         setInnSearch(false)
      }

      let isFormValid = true
      Object.keys(formControls).forEach((item) => {
         isFormValid = formControls[item].valid && isFormValid
      })

      setForm({
         formControls,
         isFormValid,
      })
   }

   useEffect(() => {
      setLoading(true)

      async function fetchData() {
         try {
            const responseCompanies = await axios.get('/companies', {
               headers: { accessToken: auth.token },
            })
            const responseReceiveTypes = await axios.get(
               '/reference-books/receive-types',
               {
                  headers: { accessToken: auth.token },
               }
            )
            const responseSourceTypes = await axios.get(
               '/reference-books/source-types',
               {
                  headers: { accessToken: auth.token },
               }
            )
            const responseContractTypes = await axios.get(
               '/reference-books/contract-types',
               {
                  headers: { accessToken: auth.token },
               }
            )
            const responseTurnoverTypes = await axios.get(
               '/reference-books/turnover-types',
               {
                  headers: { accessToken: auth.token },
               }
            )

            if (
               responseCompanies.data.is_error ||
               responseReceiveTypes.data.is_error ||
               responseSourceTypes.data.is_error ||
               responseContractTypes.data.is_error ||
               responseTurnoverTypes.data.is_error
            ) {
               setError(true)
            } else {
               setCompaniesData(responseCompanies.data.data)
               setReceiveTypesData(responseReceiveTypes.data.data)
               setSourceTypesData(responseSourceTypes.data.data)
               setContractTypesData(responseContractTypes.data.data)
               setTurnoverTypesData(responseTurnoverTypes.data.data)

               responseReceiveTypes.data.data.forEach((item) =>
                  item.is_default ? setReceiveTypes(item) : null
               )
               responseSourceTypes.data.data.forEach((item) =>
                  item.is_default ? setSourceTypes(item) : null
               )
               responseContractTypes.data.data.forEach((item) =>
                  item.is_default ? setContractTypes(item) : null
               )
               responseTurnoverTypes.data.data.forEach((item) =>
                  item.is_default ? setTurnoverTypes(item) : null
               )
            }
            setLoading(false)
         } catch (e) {
            setLoading(false)
            setError(true)
         }
      }
      fetchData()
   }, [auth.token])

   useEffect(() => {
      setAddressDisabled(true)

      async function fetchData() {
         try {
            const response = await axios.get(
               `/companies/addresses/${company.company_id || 1}`,
               {
                  headers: { accessToken: auth.token },
               }
            )

            if (response.data.is_error) {
               setError(true)
            } else {
               setAddressData(response.data.data)
               setAddressDisabled(false)
            }
         } catch (e) {
            setError(true)
         }
      }
      fetchData()
   }, [auth.token, company])

   const onSubmit = async (e) => {
      e.preventDefault()

      if (
         isEmptyObject(company) ||
         isEmptyObject(address) ||
         isEmptyObject(receiveTypes) ||
         isEmptyObject(sourceTypes) ||
         isEmptyObject(contractTypes) ||
         isEmptyObject(turnoverTypes) ||
         form.formControls.invoiceNumber.value.trim() === ''
      ) {
         return togglePopup()
      }

      const nakl = {
         provider_id: company.company_id,
         operation_date: format(getProductDate, 'yyy-MM-dd hh:mm:ss'),
         doc_date: format(invoiceDate, 'yyy-MM-dd hh:mm:ss'),
         doc_num: form.formControls.invoiceNumber.value,
         receive_type_id: receiveTypes.receive_type_id,
         source_type_id: sourceTypes.source_type_id,
         contract_type_id: contractTypes.contract_type_id,
         turnover_type_id: turnoverTypes.turnover_type_id,
         contract_num: form.formControls.contractNumber.value,
      }

      try {
         const response = await axios.post('/nakls-full', nakl, {
            headers: { accessToken: auth.token },
         })

         if (response.data.is_error) {
            setError(true)
         } else {
            history.push('/')
         }
      } catch (e) {
         setError(true)
      }
   }

   const renderInputs = () => {
      return Object.keys(form.formControls).map((item, index) => {
         const {
            label,
            value,
            type,
            valid,
            touched,
            placeholder,
            errorMessage,
            validation,
         } = form.formControls[item]

         if (index === 2) {
            return null
         }

         return (
            <Input
               key={index}
               valid={valid}
               touched={touched}
               errorMessage={errorMessage}
               shouldValidate={!!validation}
               label={label}
               type={type}
               value={value}
               placeholder={placeholder}
               disabled={index === 0 && !isEmptyObject(company)}
               onChange={(e) => onChangeHandler(e, item)}
               innSearch={index === 0 && innSearch}
               innNotFound={index === 0 && innNotFound}
            />
         )
      })
   }

   const inputs = renderInputs()

   const redirectUrlHandler = () => {
      auth.logout()
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

   if (loading) {
      return <Loader />
   }

   return (
      <div className="edit">
         <div className="container">
            <form className="edit__block" onSubmit={onSubmit}>
               <div className="edit__block-title">Создание накладной</div>
               <div className="edit__block-form">
                  <FormSelect
                     label="Поставщик"
                     data={companiesData}
                     onClick={toggleSelectCompanies}
                     state={selectCompanies}
                     changeValue={setCompanyHandler}
                     itemName="name"
                     item={company}
                  />
                  <FormSelect
                     label="Адрес поставщика"
                     data={addressData}
                     onClick={toggleSelectAddress}
                     state={selectAddress}
                     changeValue={setAddress}
                     itemName="text"
                     item={address}
                     disabled={isEmptyObject(company) || addressDisabled}
                  />
                  {inputs}
                  <SelectDate
                     data={getProductDate}
                     onChange={setGetProductDate}
                     label="Дата получения товара"
                  />
                  <SelectDate
                     data={invoiceDate}
                     onChange={setInvoiceDate}
                     label="Дата накладной"
                  />
                  <FormSelect
                     label="Вид операции отгрузки"
                     data={receiveTypesData}
                     onClick={toggleSelectReceiveTypes}
                     state={selectReceiveTypes}
                     changeValue={setReceiveTypes}
                     itemName="value"
                     item={receiveTypes}
                  />
                  <FormSelect
                     label="Источник финансирования"
                     data={sourceTypesData}
                     onClick={toggleSelectSourceTypes}
                     state={selectSourceTypes}
                     changeValue={setSourceTypes}
                     itemName="value"
                     item={sourceTypes}
                  />
                  <FormSelect
                     label="Тип контракта"
                     data={contractTypesData}
                     onClick={toggleSelectContractTypes}
                     state={selectContractTypes}
                     changeValue={setContractTypes}
                     itemName="value"
                     item={contractTypes}
                  />
                  <FormSelect
                     label="Вид операции"
                     data={turnoverTypesData}
                     onClick={toggleSelectTurnoverTypes}
                     state={selectTurnoverTypes}
                     changeValue={setTurnoverTypes}
                     itemName="value"
                     item={turnoverTypes}
                  />
                  <div className="edit__block-form-item">
                     <div className="edit__block-form-left">
                        {form.formControls.contractNumber.label}
                     </div>
                     <div className="edit__block-form-right">
                        <input
                           type={form.formControls.contractNumber.type}
                           value={form.formControls.contractNumber.value}
                           required
                           onChange={(e) =>
                              onChangeHandler(e, 'contractNumber')
                           }
                        />
                     </div>
                  </div>
               </div>
               <div className="skan__button">
                  <button
                     type="submit"
                     onClick={onSubmit}
                     className="btn skan__button-btn"
                  >
                     Отправить
                  </button>
                  <div
                     className="btn skan__button-cancel"
                     onClick={history.goBack}
                  >
                     Отмена
                  </div>
               </div>
            </form>
         </div>
         {activePopup ? (
            <Popup
               text="Заполните форму до конца!"
               active={activePopup}
               error
               toggleActive={togglePopup}
            />
         ) : null}
      </div>
   )
}

export default CreateNaklPage
