import React, { Component } from 'react';
import { Authorisation } from './Authorisation';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>
            <Authorisation />
        </div>
    );
  }
}
