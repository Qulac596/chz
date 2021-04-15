import { createContext } from 'react'

function noop() {}

// Контекст для авторизации
export const AuthContext = createContext({
   token: null,
   userId: null,
   login: noop,
   logout: noop,
   isAuth: false,
   naklId: null,
})
