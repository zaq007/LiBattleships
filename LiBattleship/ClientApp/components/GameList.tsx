import * as React from 'react';
import { NavMenu } from './NavMenu';
import { GameListModel } from '../models/GameListModel';
import * as GameListStore from '../store/GameList';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';

type GameListProps =
    GameListStore.GameListState
    & typeof GameListStore.actionCreators;

class GameList extends React.Component<GameListProps, {}> {
    public render() {
        return <ul className="game-list">
            { this.props.gameList.map( x => <li>{ x.creatorName }</li> ) }
        </ul>;
    }
}

export default connect(
    (state: ApplicationState) => state.gameList, // Selects which state properties are merged into the component's props
    GameListStore.actionCreators                 // Selects which action creators are merged into the component's props
)(GameList);