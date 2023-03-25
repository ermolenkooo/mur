import React, { Component } from 'react';
import { Redirect, Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Films } from './components/Films';
import { Serials } from './components/Serials';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/home' component={Home} />
        <Route path='/films' component={Films} />
        <Route path='/serials' component={Serials} />
        <Redirect from='/' to='/home' />
      </Layout>
    );
  }
}
