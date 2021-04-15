import axios from 'axios'
// Config
import config from '../config'

export default axios.create({
   baseURL: config.apiUrl,
})
