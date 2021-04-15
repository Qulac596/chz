import React from 'react'
import { Switch, Route, Redirect } from 'react-router-dom'
// Pages
import AuthPage from '../pages/AuthPage'
import MainPage from '../pages/MainPage'
import NaklOnePage from '../pages/NaklOnePage'
import CreateNaklPage from '../pages/CreateNaklPage'
import SkanPage from '../pages/SkanPage'
import EditNaklPage from '../pages/EditNaklPage'

export const useRoutes = (isAuth) => {
   if (isAuth) {
      return (
         <Switch>
            <Route path="/" exact>
               <MainPage />
            </Route>
            <Route path="/create" exact>
               <CreateNaklPage />
            </Route>
            <Route path="/edit" exact>
               <EditNaklPage />
            </Route>
            <Route path="/nakl-one/:id" exact>
               <NaklOnePage />
            </Route>
            <Route path="/nakl-one/:naklId/skan/:id" exact>
               <SkanPage />
            </Route>
            <Redirect to="/" />
         </Switch>
      )
   }

   return (
      <Switch>
         <Route path="/auth" exact>
            <AuthPage />
         </Route>
         <Redirect to="/auth" />
      </Switch>
   )
}
