import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { FieldHelper } from '../helpers/FieldHelper';
import { push, RouterAction } from 'react-router-redux';

export interface GameState {
    currentGameMap: Array<Array<number>>;
    enemyGameMap: Array<Array<number>>;
    isMyTurn: boolean;
    currentGameId: string;
}

interface SetGameStateAction {
    type: 'SET_GAME_STATE';
    currentGameMap: Array<Array<number>>;
    enemyGameMap: Array<Array<number>>;
    isMyTurn: boolean;
    currentGameId: string;
}


type KnownAction = SetGameStateAction | RouterAction;

export const actionCreators = {
    gameCreated: (state: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch(push(`/game/${state.id}`));
        dispatch({ type: 'SET_GAME_STATE', currentGameMap: state.myMap, enemyGameMap: state.enemyMap, isMyTurn: state.isMyTurn, currentGameId: state.id } as SetGameStateAction);
    },
    setGameState: (state: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        console.log(state);
        dispatch({ type: 'SET_GAME_STATE', currentGameMap: state.myMap, enemyGameMap: state.enemyMap, isMyTurn: state.isMyTurn, currentGameId: state.id } as SetGameStateAction);
    }
};


const unloadedState: GameState = { currentGameMap: FieldHelper.getEmptyField(), enemyGameMap: FieldHelper.getEmptyField(), isMyTurn: false, currentGameId: '' };

export const reducer: Reducer<GameState> = (state: GameState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_GAME_STATE':
            return Object.assign({}, state, action);
    }

    return state || unloadedState;
};
