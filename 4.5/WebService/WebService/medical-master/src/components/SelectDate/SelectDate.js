import React, { forwardRef } from 'react'
import DatePicker from 'react-datepicker'
import ru from 'date-fns/locale/ru'
import 'react-datepicker/dist/react-datepicker.css'
import './SelectDate.scss'

// Выбор даты для стандартного выпадающего списка
function SelectData({ onChange, data }) {
   const ExampleCustomInput = forwardRef((props, ref) => {
      return (
         <div className="Select_input" onClick={props.onClick} ref={ref}>
            {props.value}
         </div>
      )
   })

   return (
      <DatePicker
         dateFormat="dd.MM.yyyy"
         selected={data}
         onChange={(date) => onChange(date)}
         customInput={<ExampleCustomInput />}
         locale={ru}
      />
   )
}

export default SelectData
