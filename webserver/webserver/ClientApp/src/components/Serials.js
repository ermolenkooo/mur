import React, { Component } from 'react';
import { ModalEdit } from './ModalEditSerial'
import { ModalAdd } from './ModalAddSerial';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export class Serial extends Component { 

    constructor(props) {
        super(props);
        this.state = { modal: false, data: props.serial, modal1: false };
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
    onClick(e) { //удаление
        this.props.onRemove(this.state.data);
    }
    render() { 
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
                            <h5>Количество сезонов: {this.state.data.seasons}</h5>
                            <h5>Годы выхода: {this.state.data.year}</h5>
                            <h5>Возрастное ограничение: {this.state.data.age}</h5>
                            <div>
                                <ModalEdit onFilmSubmit={this.props.onUpdate} serial={this.state.data} />
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

export class Serials extends React.Component { 

    constructor(props) {
        super(props);
        this.state = { serials: [], genres: [], countries: [] };
        this.onAddSerial = this.onAddSerial.bind(this);
        this.onRemoveSerial = this.onRemoveSerial.bind(this);
        this.onUpdateSerial = this.onUpdateSerial.bind(this);
        this.loadData = this.loadData.bind(this);
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest(); //запрос на получение списка
        xhr.open("get", "/api/serials/", true);
        xhr.onload = function () {
            //console.log(xhr.responseText);
            var data = JSON.parse(xhr.responseText);
            this.setState({ serials: data });
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
    /*componentDidUpdate() {
        //alert("fjfd");
        this.loadData();
    }*/
    // добавление объекта
    onAddSerial(serial) {
        if (serial) {
            var xhr = new XMLHttpRequest();
            xhr.open("post", "/api/serials/");
            xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
            xhr.onload = function () {
                this.loadData();
            }.bind(this);
            xhr.send(JSON.stringify({ name: serial.name, original: serial.original, id_genre: serial.id_genre, year: serial.year, age: serial.age, timing: serial.timing, serial: serial.original, id_country: serial.id_country, poster: serial.poster, description: serial.description, seasons: serial.seasons }));
        }
    }
    // удаление объекта
    onRemoveSerial(serial) {
        if (serial) {
            var url = "/api/serials" + "/" + serial.id;
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
    onUpdateSerial(serial) {
        if (serial) {
            var xhr = new XMLHttpRequest();
            xhr.open("PUT", "/api/serials/" + serial.id);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                this.loadData();
                this.forceUpdate();
            }.bind(this);
            xhr.send(JSON.stringify(serial));
            //this.loadData();
        }
    }
    render() {
        var load = this.loadData;
        var remove = this.onRemoveSerial;
        var edit = this.onEditSerial;
        var update = this.onUpdateSerial;
        var mygenres = this.state.genres;
        var mycountries = this.state.countries;
        return (
            <div>
                <ModalAdd onSerialSubmit={this.onAddSerial} />
                <p></p>
                <h2><b>Список сериалов</b></h2>
                <div>
                    {
                        this.state.serials.map(function (serial) {
                            var mygenre, mycountry;
                            var i = 0;
                            for (i in mygenres) { 
                                if (mygenres[i].id == serial.id_genre)
                                    mygenre = mygenres[i].name;
                            }
                            i = 0;
                            for (i in mycountries) { 
                                if (mycountries[i].id == serial.id_country)
                                    mycountry = mycountries[i].name;
                            }
                            //для каждого элемента рендерим класс
                            return <Serial key={serial.id} serial={serial} onRemove={remove} onEdit={edit} onUpdate={update} load={load} genre={mygenre} country={mycountry} />
                        })
                    }
                </div>
            </div>
        );
    }
}