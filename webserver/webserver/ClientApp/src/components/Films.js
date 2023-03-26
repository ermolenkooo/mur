import React, { Component } from 'react';
import { ModalEdit } from './ModalEdit'
import { ModalAdd } from './ModalAdd';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export class Film extends Component { //класс фильма

    constructor(props) {
        super(props);
        this.state = { modal: false, data: props.film, modal1: false };
        this.onClick = this.onClick.bind(this);
        this.toggle = this.toggle.bind(this);
        this.toggle1 = this.toggle1.bind(this);
    }
    toggle() { //изменяем состояние отвечающее за вывод модального окна
        this.setState({
            modal: !this.state.modal
        });
    }
    toggle1() { //изменяем состояние отвечающее за вывод модального окна
        this.setState({
            modal1: !this.state.modal1
        });
    }
    onClick(e) { //удаление фильма
        this.props.onRemove(this.state.data);
    }
    render() { //каждый фильм будет представлен таблицей с двумя столбиками
        return <div>
            <table className="table table-striped table-hover" id="filmsTable" style={{ fontFamily: 'Courier New' }}>
            <tbody>
                <tr>
                    <td valign="top">
                        <img src={this.state.data.poster} style={{ width: '400px' }} />
                    </td>
                    <td valign="top">
                        <h4><b>{this.state.data.name} / {this.state.data.original}</b></h4>
                        <h5 align='justify'>{this.state.data.description}</h5>
                        <h5>Жанр: {this.props.genre}</h5>
                        <h5>Страна: {this.props.country}</h5>
                        <h5>Хронометраж: {this.state.data.timing}</h5>
                        <h5>Год выхода: {this.state.data.year}</h5>
                        <h5>Возрастное ограничение: {this.state.data.age}</h5>
                        <div>
                            <ModalEdit onFilmSubmit={this.props.onUpdate} film={this.state.data} />
                            <p></p>
                            <button className="btn btn-outline-success" onClick={this.onClick} style={{ marginRight: '10px' }}>Удалить</button>
                        </div>
                    </td>
                </tr>
            </tbody>
            </table>
        </div>;
    }
}

export class Films extends React.Component { //класс листа фильмов

    constructor(props) {
        super(props);
        this.state = { films: [], genres: [], countries: [] };
        this.onAddFilm = this.onAddFilm.bind(this);
        this.onRemoveFilm = this.onRemoveFilm.bind(this);
        this.onUpdateFilm = this.onUpdateFilm.bind(this);
        this.loadData = this.loadData.bind(this);
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest(); //запрос на получение списка фильмов
        xhr.open("get", "/api/Films/", true);
        xhr.onload = function () {
            //console.log(xhr.responseText);
            var data = JSON.parse(xhr.responseText);
            this.setState({ films: data });
        }.bind(this);
        xhr.send();

        var xhr1 = new XMLHttpRequest(); //запрос на получение списка стран
        xhr1.open("get", "/api/Countries/", true);
        xhr1.onload = function () {
            //console.log(xhr1.responseText);
            var data = JSON.parse(xhr1.responseText);
            this.setState({ countries: data });
        }.bind(this);
        xhr1.send();

        var xhr2 = new XMLHttpRequest(); //запрос на получение списка жанров
        xhr2.open("get", "/api/Genres/", true);
        xhr2.onload = function () {
            //console.log(xhr2.responseText);
            var data = JSON.parse(xhr2.responseText);
            this.setState({ genres: data });
        }.bind(this);
        xhr2.send();
    }
    componentDidMount() {
        this.loadData();
    }
    // добавление объекта
    onAddFilm(film) {
        if (film) {
            var xhr = new XMLHttpRequest();
            xhr.open("post", "/api/films/");
            xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
            xhr.onload = function () {
                this.loadData();
            }.bind(this);
            xhr.send(JSON.stringify({ name: film.name, id_genre: film.id_genre, year: film.year, age: film.age, timing: film.timing, original: film.original, id_country: film.id_country, poster: film.poster, description: film.description }));
        }
    }
    // удаление объекта
    onRemoveFilm(film) {
        if (film) {
            var url = "/api/films" + "/" + film.id;
            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                this.loadData();
            }.bind(this);
            xhr.send();
        }
    }
    //редактирование
    onUpdateFilm(film) {
        if (film) {
            var xhr = new XMLHttpRequest();
            xhr.open("PUT", "/api/films/" + film.id);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                this.loadData();
                this.forceUpdate();
            }.bind(this);
            xhr.send(JSON.stringify(film));
        }
        this.loadData();
    }
    render() {
        var load = this.loadData;
        var remove = this.onRemoveFilm;
        var edit = this.onEditFilm;
        var update = this.onUpdateFilm;
        var mygenres = this.state.genres;
        var mycountries = this.state.countries;
        return (
            <div>
                <ModalAdd onFilmSubmit={this.onAddFilm} />
                <p></p>
                <h2><b>Список фильмов</b></h2>
                <div>
                    {
                        this.state.films.map(function (film) {
                            var mygenre, mycountry;
                            var i = 0;
                            for (i in mygenres) { //определяем жанр фильма
                                if (mygenres[i].id == film.id_genre)
                                    mygenre = mygenres[i].name;
                            }
                            i = 0;
                            for (i in mycountries) { //определяем страну фильма
                                if (mycountries[i].id == film.id_country)
                                    mycountry = mycountries[i].name;
                            }
                            //для каждого элемента рендерим класс film
                            return <Film key={film.id} film={film} onRemove={remove} onEdit={edit} onUpdate={update} load={load} genre={mygenre} country={mycountry} />
                        })
                    }
                </div>
            </div>
        );
    }
}