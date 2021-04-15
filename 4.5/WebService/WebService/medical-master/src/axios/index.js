import axios from 'axios'
// Config
import config from '../config'

// Конфиг запроса АПИ
export default axios.create({
   baseURL: config.apiUrl,
})
