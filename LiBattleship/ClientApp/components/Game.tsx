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
    & RouteComponentProps<{
        id: string
    }>;

class Game extends React.Component<GameProps, {}> {
    componentDidMount() {
        if (!this.props[this.props.match.params.id]) {
            GameService.GetGameState(this.props.match.params.id).then(state => {
                this.props.setGameState(state);
            }).catch(() => {
                this.props.history.push('/');
            });
        }
    }

    constructor(props: any) {
        super(props);

        this.onEnemyFieldClick = this.onEnemyFieldClick.bind(this);
    }

    render() {
        const gameId = this.props.match.params.id;
        if (this.props[gameId])
            return <div>
                <GameField field={this.props[gameId].myMap} readonly={true} />
                <GameField field={this.props[gameId].enemyMap} readonly={!this.props[gameId].isMyTurn} onClick={this.onEnemyFieldClick} />
            </div>;
        else
            return <div />;
    }

    onEnemyFieldClick(x: number, y: number) {
        GameService.MakeMove(this.props.match.params.id, x, y).then((state) => this.props.setGameState(state));
    }
}

export default connect(
    (state: ApplicationState) => state.game, // Selects which state properties are merged into the component's props
    GameStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Game) as typeof Game;
