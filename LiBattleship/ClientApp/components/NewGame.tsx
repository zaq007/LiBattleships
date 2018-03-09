import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as NewGameStore from '../store/NewGame';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameField from './GameField';

type NewGameProps =
    NewGameStore.NewGameState
    & typeof NewGameStore.actionCreators
    & RouteComponentProps<{}>;

class NewGame extends React.Component<NewGameProps, {}> {

    render() {
        console.log(this.props.newGameMap )
        return <div>
            <GameField field={ this.props.newGameMap } />
            </div>;
    }
}

export default connect(
    (state: ApplicationState) => state.newGame, // Selects which state properties are merged into the component's props
    NewGameStore.actionCreators                 // Selects which action creators are merged into the component's props
)(NewGame) as typeof NewGame;
