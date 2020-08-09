import React, {useEffect, useState} from 'react';
import {Redirect, Route, Switch} from 'react-router-dom';
import {useDispatch} from 'react-redux';
import SignIn from './pages/sign_in/SignIn';
import SignUp from './pages/sign_up/SignUp';
import BaseLayout from './components/base_layout/BaseLayout';
import {storeUserData} from './redux/actions/user_data_actions';
import './App.css';

const App = () => {
  const [userLoginStatus, setUserLoginStatus] = useState({loading: true, isUserLoggedIn: false});
  const dispatch = useDispatch();

  const {loading, isUserLoggedIn} = userLoginStatus;

  useEffect(() => {
    const userData = localStorage.getItem('userData');
    if (userData) {
      dispatch(storeUserData(JSON.parse(userData)));
      setUserLoginStatus({
        loading: false,
        isUserLoggedIn: true,
      });
    } else setUserLoginStatus(prevData => ({
      ...prevData,
      loading: false,
    }));
  }, []);

  if (loading) return <p>Loading</p>;

  return (
      <Switch>
        <Route path="/" exact>
          {isUserLoggedIn ? <Redirect to="/home"/> : <Redirect to='/sign-in'/>}
        </Route>
        <Route path="/home/">
          {isUserLoggedIn ? <BaseLayout/> : <Redirect to="/sign-in/"/>}
        </Route>
        <Route path="/sign-in/" exact>
          {isUserLoggedIn ? <Redirect to='/home/'/> : <SignIn/>}
        </Route>
        <Route path="/sign-up/">
          {isUserLoggedIn ? <Redirect to='/home/'/> : <SignUp/>}
        </Route>
      </Switch>
  );
};

export default App;
