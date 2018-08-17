import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as GameStore from '../store/Game';
import * as NewGameStore from '../store/NewGame';
import * as Router from '../store/Router';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameField from './GameField';
import { GameService } from '../services/GameService';
import { FieldChecker } from '../helpers/FieldCheck';

type JoinGameProps =
    NewGameStore.NewGameState
    & typeof GameStore.actionCreators
    & typeof Router.actionCreators
    & RouteComponentProps<{ id: string }>;

interface JoinGameState {
    isValid: boolean;
}

class JoinGame extends React.Component<JoinGameProps, JoinGameState> {

    constructor() {
        super();
        this.state = { isValid: false }
    }

    render() {
        return <div>
            <GameField field={this.props.newGameMap} onClick={this.onFieldClick} />
            <button onClick={this.joinGame} disabled={!this.state.isValid}>Create Game</button>
        </div>;
    }

    joinGame = () => {
        GameService.JoinGame(this.props.match.params.id, this.props.newGameMap)
            .then((result) => this.props.gameCreated(result)).catch(() => this.props.history.push('/'));
    }

    onFieldClick = () => { this.setState({ isValid: FieldChecker.check(this.props.newGameMap).isValid}) }
}

export default connect(
    (state: ApplicationState) => state.newGame, // Selects which state properties are merged into the component's props
    Object.assign({}, GameStore.actionCreators, Router.actionCreators)                 // Selects which action creators are merged into the component's props
)(JoinGame) as typeof JoinGame;
