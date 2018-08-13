import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { FieldChecker } from '../helpers/FieldCheck';

export interface GameState {
    currentGameMap: Array<Array<number>>;
    enemyGameMap: Array<Array<number>>;
}

interface MakeShootAction {
    type: 'MAKE_SHOOT';
}

interface GotDataAction {
    type: 'GOT_DATA';
}

type KnownAction = MakeShootAction | GotDataAction;

export const actionCreators = {
    makeShoot: (x: number, y: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'MAKE_SHOOT' });
    }
};


const unloadedState: GameState = { currentGameMap: FieldChecker.getEmptyField(), enemyGameMap: FieldChecker.getEmptyField() };

export const reducer: Reducer<GameState> = (state: GameState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'MAKE_SHOOT':
            return Object.assign({}, state);
        case 'GOT_DATA':
            return Object.assign({}, state);
        default:
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
