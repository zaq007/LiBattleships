import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { FieldChecker } from '../helpers/FieldCheck';
import { GameListModel } from '../models/GameListModel';

export interface GameListState {
    gameList: Array<GameListModel>;
}

type KnownAction = {};

export const actionCreators = {

};


const unloadedState: GameListState = { gameList: [] };

export const reducer: Reducer<GameListState> = (state: GameListState, incomingAction: Action) => {
    //const action = incomingAction as KnownAction;
    //switch (action.type) {
    //    default:
    //        const exhaustiveCheck = action;
    //}
    //
    return state || unloadedState;
};
