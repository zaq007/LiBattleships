import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { FieldHelper } from '../helpers/FieldHelper';
import { push, RouterAction } from 'react-router-redux';

export interface State {
    myMap: Array<Array<number>>;
    enemyMap: Array<Array<number>>;
    isMyTurn: boolean;
    id: string;
}

export interface GameState {
    [id: string]: State;
}

interface SetGameStateAction {
    type: 'SET_GAME_STATE';
    state: State;
}


type KnownAction = SetGameStateAction | RouterAction;

export const actionCreators = {
    gameCreated: (state: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_GAME_STATE', state } as SetGameStateAction);
        dispatch(push(`/game/${state.id}`));
    },
    setGameState: (state: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_GAME_STATE', state } as SetGameStateAction);
    }
};


const unloadedState: GameState = { };

export const reducer: Reducer<GameState> = (state: GameState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_GAME_STATE':
            const setGameStateAction = action as SetGameStateAction;
            return Object.assign({}, state, {
                [setGameStateAction.state.id]: setGameStateAction.state
            });
    }

    return state || unloadedState;
};
