import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as LoginStore from '../store/Login';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameCell from './GameCell';
import { Field } from '../models/Field';
//type LoginProps =
//    LoginStore.LoginState
//    & typeof LoginStore.actionCreators
//    & RouteComponentProps<{}>;

type GameFieldProps = {
    field: Field;
    readonly?: boolean
    onClick?: Function;
}

export default class GameField extends React.Component<GameFieldProps, {}> {
    readonly: boolean;

    constructor(props: GameFieldProps) {
        super(props);
        this.readonly = !!this.props.readonly;
        console.log(props)
    }

    public render() {
        console.log(this.props)
        var cells = [];
        for (let i = 0; i < 100; i++) {
            cells.push(<GameCell x={i % 10} y={Math.trunc(i / 10)} key={i} clickHandler={this.cellClickHandler.bind(this)} field={this.props.field} />);
        }
        return <div className="gameField">{cells}</div>;
    }

    cellClickHandler(x: number, y: number): boolean {
        if (!this.readonly && !this.props.field.makeAction(x,y)) return false;
        if (this.props.onClick) this.props.onClick(x, y);
        return true;
    }    
}

//export default connect(
//    (state: ApplicationState) => state.login, // Selects which state properties are merged into the component's props
//    LoginStore.actionCreators                 // Selects which action creators are merged into the component's props
//)(Login);
