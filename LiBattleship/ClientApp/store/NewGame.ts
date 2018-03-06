import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { FieldChecker } from '../helpers/FieldCheck';

export interface NewGameState {
    newGameMap: Array<Array<number>>;
}

interface NewGameCreatedAction {
    type: 'NEW_GAME_CREATED';
}

interface NewGameIDGotAction {
    type: 'NEW_GAME_GET';
}

type KnownAction = NewGameCreatedAction | NewGameIDGotAction;

export const actionCreators = {
    newGame: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'NEW_GAME_CREATED' });
    }
};


const unloadedState: NewGameState = { newGameMap: FieldChecker.getEmptyField() };

export const reducer: Reducer<NewGameState> = (state: NewGameState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'NEW_GAME_CREATED':
            return Object.assign({}, state);
        case 'NEW_GAME_GET':
            return Object.assign({}, state);
        default:
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
