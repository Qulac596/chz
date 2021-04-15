import React, { forwardRef } from 'react'
import DatePicker from 'react-datepicker'
import ru from 'date-fns/locale/ru'
import 'react-datepicker/dist/react-datepicker.css'
import './SelectDate.scss'

// Выбор даты
function SelectData({ onChange, data, label }) {
   const ExampleCustomInput = forwardRef((props, ref) => {
      return (
         <div
            className="edit__block-selector edit__block-form-right"
            onClick={props.onClick}
            ref={ref}
         >
            {props.value}
         </div>
      )
   })

   return (
      <div className="edit__block-form-item">
         <div className="edit__block-form-left">{label}</div>
         <div className="edit__block-form-right">
            <DatePicker
               dateFormat="dd.MM.yyyy"
               selected={data}
               onChange={(date) => onChange(date)}
               customInput={<ExampleCustomInput />}
               locale={ru}
            />
         </div>
      </div>
   )
}

export default SelectData
