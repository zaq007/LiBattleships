import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as LoginStore from '../store/Login';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';

type LoginProps =
    LoginStore.LoginState
    & typeof LoginStore.actionCreators
    & RouteComponentProps<{}>;

class Login extends React.Component<LoginProps, {}> {
    public render() {
        return this.props.token ? this.getLoggedUser() : this.getLoginForm();
    }

    private getLoggedUser() {
        return <span>{this.props.token}</span>;
    }

    private getLoginForm() {
        return <form id="signin" className="navbar-form navbar-right" role="form" onSubmit={this.loginHandler.bind(this)} >
            <div className="input-group">
                <span className="input-group-addon"><i className="glyphicon glyphicon-user"></i></span>
                <input id="email" className="form-control" name="email" placeholder="Email Address" type="email" />
            </div>

            <div className="input-group">
                <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                <input id="password" className="form-control" name="password" placeholder="Password" type="password" />
            </div>

            <button type="submit" className="btn btn-primary">Login</button>
        </form>;
    }

    private loginHandler(e: any) {
        e.preventDefault();
        this.props.sendCredentials("123", "123");
    }

}

export default connect(
    (state: ApplicationState) => state.login, // Selects which state properties are merged into the component's props
    LoginStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Login);
