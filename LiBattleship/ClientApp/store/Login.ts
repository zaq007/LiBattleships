import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface LoginState {
    token?: string | null;
}

interface RequestTokenAction {
    type: 'REQUEST_TOKEN';
    login: string;
    password: string;
}

interface ReceiveTokenAction {
    type: 'RECEIVE_TOKEN';
    token: string;
}

type KnownAction = RequestTokenAction | ReceiveTokenAction;

export const actionCreators = {
    sendCredentials: (login: string, password: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        console.log(login, password);
        dispatch({ type: 'RECEIVE_TOKEN', token: login + '_TEST_' + password });
    }
};


const unloadedState: LoginState = { token: null };

export const reducer: Reducer<LoginState> = (state: LoginState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'RECEIVE_TOKEN':
            return Object.assign({}, state, { token: action.token });
        case 'REQUEST_TOKEN':
            return Object.assign({}, state);
        default:
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
