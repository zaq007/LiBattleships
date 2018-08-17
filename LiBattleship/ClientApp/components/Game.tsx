import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as GameStore from '../store/Game';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameField from './GameField';
import { GameService } from '../services/GameService';

type GameProps =
    GameStore.GameState
    & typeof GameStore.actionCreators
    & RouteComponentProps<{}>;

class Game extends React.Component<GameProps, {}> {
    render() {
        return <div>
            <GameField field={this.props.currentGameMap} readonly={true} />
            <GameField field={this.props.enemyGameMap} readonly={ !this.props.isMyTurn } onClick={this.onEnemyFieldClick.bind(this)} />
                </div>;
    }

    onEnemyFieldClick(x: number, y: number) {
        GameService.MakeMove(this.props.currentGameId, x, y).then((state) => this.props.setGameState(state));
    }
}

export default connect(
    (state: ApplicationState) => state.game, // Selects which state properties are merged into the component's props
    GameStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Game) as typeof Game;
