// Utils
import { getYears } from '../utils'

// Данные
export const months = [
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

export const years = getYears(2002)
