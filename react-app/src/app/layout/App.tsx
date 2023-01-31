import React from 'react';
import Intro from './Intro';
import { Route, Switch } from 'react-router-dom';
function App() {
  return (
    <Switch>
      <Route exact path='/' component={Intro} />
    </Switch>
  );
}

export default App;
