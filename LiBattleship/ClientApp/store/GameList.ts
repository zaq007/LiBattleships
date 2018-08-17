import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { FieldChecker } from '../helpers/FieldCheck';
import { GameListModel } from '../models/GameListModel';

export interface GameListState {
    gameList: Array<GameListModel>;
}

interface SetGameList {
    type: 'SET_GAME_LIST';
    gameList: Array<GameListModel>; 
}

type KnownAction = SetGameList;

export const actionCreators = {
    setGameList: (list: any[]): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({
            type: 'SET_GAME_LIST', gameList: list.map(x => {
                const model = new GameListModel();
                model.creatorName = x.id;
                model.guid = x.id;
                return model;
            })
        });
    },
};


const unloadedState: GameListState = { gameList: [] };

export const reducer: Reducer<GameListState> = (state: GameListState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_GAME_LIST':
            return Object.assign({}, state, { gameList: action.gameList });
        default:
            const exhaustiveCheck = action;
    }
    
    return state || unloadedState;
};
