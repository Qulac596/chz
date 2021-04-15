import React, { useState, useContext } from 'react'
import axios from '../axios'
// Images
import logoImg from '../assets/images/logo.png'
// Utils
import { validateControl } from '../utils'
// Components
import Input from '../components/pages/AuthPage/Input/Input'
import Popup from '../components/Popup/Popup'
// Context
import { AuthContext } from '../context/AuthContext'

// Страница авторизации
function AuthPage() {
   const [form, setForm] = useState({
      isFormValid: false,
      formControls: {
         username: {
            value: '',
            type: 'text',
            label: 'Введите логин',
            placeholder: 'логин',
            errorMessage: 'Введите корректно логин',
            valid: false,
            touched: false,
            validation: {
               required: true,
            },
         },
         password: {
            value: '',
            type: 'password',
            label: 'Введите пароль',
            placeholder: '*****',
            errorMessage: 'Введите корректно пароль',
            valid: false,
            touched: false,
            validation: {
               required: true,
            },
         },
      },
   })
   const [error, setError] = useState(false)
   const [popup, setPopup] = useState(false)
   const [popupText, setPopupText] = useState('')
   const auth = useContext(AuthContext)

   const toggleActivePopup = () => {
      setPopup(!popup)
   }

   const onChangeHandler = (e, item) => {
      const formControls = { ...form.formControls }
      const inputItem = formControls[item]
      inputItem.value = e.target.value
      inputItem.touched = true
      inputItem.valid = validateControl(inputItem.value, inputItem.validation)

      let isFormValid = true
      Object.keys(formControls).forEach((item) => {
         isFormValid = formControls[item].valid && isFormValid
      })

      setForm({
         formControls,
         isFormValid,
      })
   }

   const onSubmit = async (e) => {
      e.preventDefault()

      setError(false)
      setPopupText('')

      const authData = {
         login: form.formControls.username.value,
         password: form.formControls.password.value,
      }

      try {
         const response = await axios.put('/users/enter', null, {
            params: authData,
         })

         if (response.data.is_error) {
            setPopupText(response.data.error_message)
            setError(true)
            setPopup(true)
         } else {
            auth.login(response.data.data.access_token, response.data.data.fio)
         }
      } catch (e) {
         setPopupText('Что-то пошло не так!')
         setError(true)
         setPopup(true)
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
               onChange={(e) => onChangeHandler(e, item)}
               autoFocus={index === 0}
            />
         )
      })
   }

   const inputs = renderInputs()

   return (
      <div className="authoriz">
         <div className="container">
            <form className="authoriz__wrapper" onSubmit={onSubmit}>
               <div className="authoriz__logo logo">
                  <img src={logoImg} alt="" />
               </div>
               <div className="authoriz__title">Авторизация</div>
               {inputs}
               <button
                  onClick={onSubmit}
                  type="submit"
                  className="btn authoriz__btn"
               >
                  Продолжить
               </button>
            </form>
         </div>
         {error ? (
            <Popup
               text={popupText}
               active={popup}
               error={error}
               toggleActive={toggleActivePopup}
            />
         ) : null}
      </div>
   )
}

export default AuthPage
