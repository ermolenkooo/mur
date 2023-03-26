/*import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}*/

import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Authorisation } from './Authorisation';

export class Layout extends Component {
    static displayName = Layout.name;

    constructor(props) {
        super(props);
        this.state = {
            isLoggedIn: ""
        };
    }

    componentDidMount() {
        this.getData();
    }

    async getData() {
        const token = sessionStorage.getItem('tokenKey');
        const response = await fetch("/api/Authorization/getlogin", {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token  // передача токена в заголовке
            }
        });
        if (response.ok === true) {
            {
                this.setState({ isLoggedIn: true });
            }
        }
        else {
            console.log("Status: ", response.status);
            this.setState({ isLoggedIn: false });
        }
    };

    renderNav() {
        return (<NavMenu isLoggedIn={ this.state.isLoggedIn } />);
    }

    render() {
        if (this.state.isLoggedIn === false) {
            return (
                <div>
                    <div>{this.renderNav()}</div>
                    <Authorisation />
                </div>
            );
        }
        if (this.state.isLoggedIn === true) {
            return (
                <div>
                    <div>{this.renderNav()}</div>
                    <Container>
                        {this.props.children}
                    </Container>
                </div>
            );
        }
        if (this.state.isLoggedIn === "")
            return (<h2>Loading...</h2>);
    }
}
