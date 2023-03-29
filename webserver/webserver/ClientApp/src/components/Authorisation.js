import React, { Component } from 'react';
import { Alert, InputGroup, InputGroupAddon } from 'reactstrap';
import '../custom.css'

export class Authorisation extends Component {
    static displayName = Authorisation.name;

    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            alertVisible: false
        };

        this.onSubmit = this.onSubmit.bind(this);
        this.onEmailChange = this.onEmailChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);
        this.onChangeAlert = this.onChangeAlert.bind(this);
    }

    onEmailChange(e) {
        this.setState({ email: e.target.value });
    }
    onPasswordChange(e) {
        this.setState({ password: e.target.value });
    }
    onChangeAlert(value) {
        this.setState({ alertVisible: value });
    }

    // Вход
    onSubmit(e) {
        e.preventDefault();
        if (this.state.email && this.state.password) {
            this.getTokenAsync();
        }
    }

    async getTokenAsync() {

        const formData = new FormData();
        formData.append("grant_type", "password");
        formData.append("username", this.state.email);
        formData.append("password", this.state.password);
        formData.append("role", "admin");

        // отправляет запрос и получаем ответ
        const response = await fetch("/token", {
            method: "POST",
            headers: { "Accept": "application/json" },
            body: formData
        });
        // получаем данные 
        const data = await response.json();

        if (response.ok === true) {
            // сохраняем в хранилище sessionStorage токен доступа
            sessionStorage.setItem('accessToken', data.access_token);
            window.location.reload();
        }
        else {
            console.log("Error: ", response.status, data.errorText);
            if (data.errorText === "Invalid username or password.")
                this.setState({ alertVisible: true });
        }
    }

    render() {
        return (
            <div style={ { marginRight: 300 + 'px', marginLeft: 300 + 'px' } }>
                <div className="loginBody">
                    <form onSubmit={this.onSubmit}>
                        <Alert color="danger" isOpen={this.state.alertVisible} toggle={() => { this.onChangeAlert(false) }} fade={false}>Неверный логин или пароль!</Alert >
                        <h2>Авторизация</h2>
                        <br />

                        <div className="form-group">
                            <label for="InputEmail" className="control-label-required">Введите имя пользователя:</label>
                            <input type="text" required className="form-control" id="InputEmail" placeholder="Имя пользователя" value={this.state.email} onChange={this.onEmailChange} />
                        </div>
                        <div className="form-group">
                            <label for="InputPassword" className="control-label-required">Введите пароль:</label>
                            <input type="password" required className="form-control" id="InputPassword" placeholder="Пароль" value={this.state.password} onChange={this.onPasswordChange} />
                        </div>

                        <br />
                        <input color="success" className="btn btn-success" type="submit" value="Войти" />
                    </form>
                </div>
                <br />
            </div>
        );
    }
}