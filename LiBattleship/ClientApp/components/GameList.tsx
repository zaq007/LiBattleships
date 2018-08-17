import * as React from 'react';
import { NavMenu } from './NavMenu';
import { GameListModel } from '../models/GameListModel';
import * as GameListStore from '../store/GameList';
import * as Router from '../store/Router';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { GameService } from '../services/GameService';
import { RouteComponentProps } from 'react-router-dom';

type GameListProps =
    GameListStore.GameListState
    & typeof GameListStore.actionCreators
    & typeof Router.actionCreators;

class GameList extends React.Component<GameListProps, {}> {
    componentDidMount() {
        GameService.GetAvailableGames().then((list) => this.props.setGameList(list));
    }

    public render() {
        return <ul className="game-list">
            {this.props.gameList.map(x => <li key={x.guid} onDoubleClick={() => this.joinGame(x.guid) }>{x.creatorName}</li>)}
        </ul>;
    }

    joinGame = (id: string) => this.props.push(`/join/${id}`);
}

export default connect(
    (state: ApplicationState) => state.gameList, // Selects which state properties are merged into the component's props
    Object.assign({}, GameListStore.actionCreators, Router.actionCreators)              // Selects which action creators are merged into the component's props
)(GameList);