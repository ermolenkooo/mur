import React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export class ModalAdd extends React.Component { //класс модального окна добавления фильма
    constructor(props) {
        super(props);
        this.state = { modal: false, name: "", timing: "", year: "", age: "", original: "", id_genre: "", id_country: "", description: "", poster: "", genres: [], countries: [] };
        this.fileInput = React.createRef();
        this.toggle = this.toggle.bind(this);
        this.handleChangeName = this.handleChangeName.bind(this);
        this.handleChangeYear = this.handleChangeYear.bind(this);
        this.handleChangeAge = this.handleChangeAge.bind(this);
        this.handleChangeOriginal = this.handleChangeOriginal.bind(this);
        this.handleChangeTiming = this.handleChangeTiming.bind(this);
        this.handleChangeGenre = this.handleChangeGenre.bind(this);
        this.handleChangeCountry = this.handleChangeCountry.bind(this);
        this.handleChangeDescription = this.handleChangeDescription.bind(this);
        this.handleChangePoster = this.handleChangePoster.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }
    handleChangeName(event) {
        this.setState({ name: event.target.value });
    }
    handleChangeTiming(event) {
        this.setState({ timing: event.target.value });
    }
    handleChangeGenre(event) {
        this.setState({ id_genre: event.target.value });
    }
    handleChangeCountry(event) {
        this.setState({ id_country: event.target.value });
    }
    handleChangeDescription(event) {
        this.setState({ description: event.target.value });
    }
    handleChangeYear(event) {
        this.setState({ year: event.target.value });
    }
    handleChangeAge(event) {
        this.setState({ age: event.target.value });
    }
    handleChangePoster(event) {
        this.setState({ poster: event.target.value });
    }
    handleChangeOriginal(event) {
        this.setState({ original: event.target.value });
    }
    handleSubmit(e) { //обрабатываем клик на кнопку "добавить"
        e.preventDefault();
        var filmName = this.state.name.trim();
        var filmTiming = this.state.timing.trim();
        var filmGenre = this.state.id_genre.trim();
        var filmCountry = this.state.id_country.trim();
        var filmDescription = this.state.description.trim();
        var filmPoster = "D:/университет/то что нельзя называть/постеры/" + this.fileInput.current.files[0].name;
        var filmYear = this.state.year.trim();
        var filmAge = this.state.age.trim();
        var filmOriginal = this.state.original.trim();
        if (!filmName || !filmDescription || !filmTiming || !filmGenre || !filmCountry || !filmYear || !filmAge || !filmOriginal) {
            return;
        }
        this.props.onFilmSubmit({ name: filmName, description: filmDescription, timing: filmTiming, id_genre: filmGenre, id_country: filmCountry, poster: filmPoster, year: filmYear, age: filmAge, original: filmOriginal });
        this.setState({ name: "", timing: "", id_genre: "", id_country: "", description: "", poster: "", year: "", age: "", original: "" });
        this.toggle();
    }
    // загрузка данных
    loadData() {
        var xhr1 = new XMLHttpRequest(); //получение списка стран
        xhr1.open("get", "/api/countries/", true);
        xhr1.onload = function () {
            var data = JSON.parse(xhr1.responseText);
            this.setState({ countries: data });
        }.bind(this);
        xhr1.send();

        var xhr2 = new XMLHttpRequest(); //получение списка жанров
        xhr2.open("get", "/api/genres/", true);
        xhr2.onload = function () {
            var data = JSON.parse(xhr2.responseText);
            this.setState({ genres: data });
        }.bind(this);
        xhr2.send();
    }
    componentDidMount() {
        this.loadData();
    }

    render() {
        return (
            <div>
                <Button color="success" onClick={this.toggle}>Добавить фильм</Button>
                <Modal isOpen={this.state.modal}>
                    <form onSubmit={this.handleSubmit}>
                        <ModalHeader><h5>Добавление фильма</h5></ModalHeader>

                        <ModalBody>
                            <form>
                                <div className="form-group">
                                    <label className="control-label required">Название</label>
                                    <input type="text"
                                        className="form-control" id="InputName" required="required" placeholder="Введите название"
                                        value={this.state.name}
                                        onChange={this.handleChangeName} />
                                </div>

                                <div className="form-group">
                                    <label className="control-label required">Оригинальное название</label>
                                    <input type="text"
                                        className="form-control" id="InputOriginal" required="required" placeholder="Введите оригинальное название"
                                        value={this.state.original}
                                        onChange={this.handleChangeOriginal} />
                                </div>

                                <div className="form-group">
                                    <label className="control-label required">Жанр</label>
                                    <select className="form-control" id="InputGenre" required="required" placeholder="Выберете жанр"
                                        value={this.state.id_genre}
                                        onChange={this.handleChangeGenre}>
                                        <option>Выберите жанр</option>
                                        {
                                            this.state.genres.map(function (genre) {
                                                return <option value={genre.id}>{genre.name}</option>
                                            })
                                        }
                                    </select>
                                </div>
                                <div className="form-group">
                                    <label className="control-label required">Хронометраж</label>
                                    <input typeName="text"
                                        class="form-control" id="InputTiming" required="required" placeholder="Введите хронометраж"
                                        value={this.state.timing}
                                        onChange={this.handleChangeTiming} />
                                </div>

                                <div className="form-group">
                                    <label className="control-label required">Год выхода</label>
                                    <input typeName="text"
                                        class="form-control" id="InputYear" required="required" placeholder="Введите год выхода"
                                        value={this.state.year}
                                        onChange={this.handleChangeYear} />
                                </div>

                                <div className="form-group">
                                    <label className="control-label required">Возрастное ограничение</label>
                                    <input typeName="text"
                                        class="form-control" id="InputAge" required="required" placeholder="Введите возрастное ограничение"
                                        value={this.state.age}
                                        onChange={this.handleChangeAge} />
                                </div>

                                <div className="form-group">
                                    <label className="control-label required">Страна</label>
                                    <select className="form-control" id="InputCountry" required="required"
                                        value={this.state.id_country}
                                        onChange={this.handleChangeCountry}>
                                        <option>Выберите страну</option>
                                        {
                                            this.state.countries.map(function (country) {
                                                return <option value={country.id}>{country.name}</option>
                                            })
                                        }
                                    </select>
                                </div>
                                <div className="form-group">
                                    <label className="control-label required">Описание</label>
                                    <textarea className="form-control" id="InputDescrip" placeholder="Введите описание"
                                        value={this.state.description}
                                        onChange={this.handleChangeDescription}>
                                    </textarea>
                                </div>

                                <div className="form-group">
                                    <label className="control-label required">Постер</label>
                                    <p></p>
                                    <input type="file" ref={this.fileInput} id="InputPoster" onChange={this.handleChangePoster} />
                                </div>

                            </form>
                        </ModalBody>

                        <ModalFooter>
                            <input type="submit" value="Добавить" color="success" className="btn btn-success" />
                            <Button color="danger" onClick={this.toggle}>Отмена</Button>
                        </ModalFooter>
                    </form>
                </Modal>
            </div>
        );
    }
}
