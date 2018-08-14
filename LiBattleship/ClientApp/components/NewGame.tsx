import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as NewGameStore from '../store/NewGame';
import * as Router from '../store/Router';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameField from './GameField';
import { GameService } from '../services/GameService';
import { FieldChecker } from '../helpers/FieldCheck';

type NewGameProps =
    NewGameStore.NewGameState
    & typeof NewGameStore.actionCreators
    & typeof Router.actionCreators
    & RouteComponentProps<{}>;

interface NewGameState {
    isValid: boolean;
}

class NewGame extends React.Component<NewGameProps, NewGameState> {

    constructor() {
        super();
        this.state = { isValid: false }
    }

    render() {
        return <div>
            <GameField field={this.props.newGameMap} onClick={this.onFieldClick} />
            <button onClick={this.newGame} disabled={!this.state.isValid}>Create Game</button>
        </div>;
    }

    newGame = () => { GameService.CreateGame(this.props.newGameMap).then(() => this.props.push('/')) }

    onFieldClick = () => { this.setState({ isValid: FieldChecker.check(this.props.newGameMap).isValid}) }
}

export default connect(
    (state: ApplicationState) => state.newGame, // Selects which state properties are merged into the component's props
    Object.assign({}, NewGameStore.actionCreators, Router.actionCreators)                 // Selects which action creators are merged into the component's props
)(NewGame) as typeof NewGame;
