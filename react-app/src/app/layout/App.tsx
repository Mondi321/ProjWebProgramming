import React, { useEffect } from 'react';
import Intro from './Intro';
import { Route, Switch } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import Forms from '../../features/users/Forms';
import Home from '../../features/home/Home';
import NotFound from '../../features/errors/NotFound';
import ServerError from '../../features/errors/ServerError';
import { ToastContainer } from 'react-toastify';
import { useStore } from '../stores/store';
import LoadingComponent from './LoadingComponent';
import TestErrors from '../../features/errors/TestError';

function App() {
  const { commonStore, userStore } = useStore();

  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore])

  if (!commonStore.appLoaded) return <LoadingComponent />

  return (
    <>
      <ToastContainer position='bottom-right' hideProgressBar />
      <Switch>
        <Route exact path='/' component={Intro} />

        <Route
          path="(/sign-in|/sign-up)"
          render={() => (
            <Forms />
          )}
        />
        <Route
          path='/home' component={Home}
        />

        <Route
          path='/server-error' component={ServerError}
        />

        <Route 
          path='/errors' component={TestErrors}
        />
        <Route component={NotFound} />
      </Switch>
    </>
  );
}

export default observer(App);
