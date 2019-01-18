import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as LoginStore from '../store/Login';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import { fetch } from 'domain-task';
import { AuthService } from '../services/AuthService';

type LoginProps =
    LoginStore.LoginState
    & typeof LoginStore.actionCreators
    & RouteComponentProps<{}>;

type LoginState = {
    username: string;
    password: string;
}

class Login extends React.Component<LoginProps, LoginState> {
    public componentDidMount() {
        AuthService.getToken().then(token => {
            localStorage.setItem("authToken", token);
            this.props.receivedToken(token);
        });
    }

    constructor(props: any) {
        super(props);

        this.state = {
            username: '',
            password: ''
        }

        this.loginHandler = this.loginHandler.bind(this);
        this.handleUserNameChange = this.handleUserNameChange.bind(this); 
        this.handlePassChange = this.handlePassChange.bind(this);
        this.logOut = this.logOut.bind(this);
    }

    public render() {
        return this.props.isLoggedIn ? this.getLoggedUser() : this.getLoginForm();
    }

    private getLoggedUser() {
        return <div>
            <span>Hey, {this.props.token && this.props.token.Username}</span>
            <button onClick={this.logOut} className="btn btn-primary">Log Out</button>
        </div>;
    }

    private getLoginForm() {
        return <form id="signin" className="navbar-form navbar-right" role="form" onSubmit={this.loginHandler} >
            <div className="input-group">
                <span className="input-group-addon"><i className="glyphicon glyphicon-user"></i></span>
                <input id="username" className="form-control" name="username" placeholder="Username" type="text" value={this.state.username} onChange={this.handleUserNameChange} />
            </div>

            <div className="input-group">
                <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                <input id="password" className="form-control" name="password" placeholder="Password" type="password" value={this.state.password} onChange={this.handlePassChange} />
            </div>

            <button type="submit" className="btn btn-primary">Login</button>
        </form>;
    }

    private loginHandler(e: any) {
        e.preventDefault();
        AuthService.Auth(this.state.username, this.state.password).then((token) => {
            localStorage.setItem("authToken", token);
            this.props.receivedToken(token);
        });
    }

    private logOut() {
        localStorage.removeItem("authToken");
        AuthService.Auth().then((token) => {
            localStorage.setItem("authToken", token);
            this.props.receivedToken(token);
        });
    }

    private handleUserNameChange(event: any) {
        this.setState({ username: event.target.value });
    }

    private handlePassChange(event: any) {
        this.setState({ password: event.target.value });
    }
}

export default connect(
    (state: ApplicationState) => state.login, // Selects which state properties are merged into the component's props
    LoginStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Login);
