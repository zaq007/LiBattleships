import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as LoginStore from '../store/Login';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import { FieldHelper } from '../helpers/FieldHelper';

//type LoginProps =
//    LoginStore.LoginState
//    & typeof LoginStore.actionCreators
//    & RouteComponentProps<{}>;

type CellProps = {
    x: number,
    y: number,
    states: number[][],
    clickHandler?: Function
};

export default class GameCell extends React.Component<CellProps, {}> {
    getCss() {
        const classes = ['gameField--cell'];
        const cellValue = this.props.states[this.props.x][this.props.y];
        if (FieldHelper.getShipSize(cellValue) > 0) classes.push('gameField--cell--selected');
        return classes.join(' ');
    }

    onClick() {
        let isValid = true;
        if (this.props.clickHandler) {
            isValid = this.props.clickHandler(this.props.x, this.props.y)
        }
        if (isValid) this.forceUpdate();
    }

    public render() {
        return <div className={this.getCss()} onClick={ this.onClick.bind(this) }></div>;
    }
}

//export default connect(
//    (state: ApplicationState) => state.login, // Selects which state properties are merged into the component's props
//    LoginStore.actionCreators                 // Selects which action creators are merged into the component's props
//)(Login);
