﻿import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { JwtToken } from '../helpers/JwtToken';
import { BaseService } from '../services/BaseService';

export interface LoginState {
    token?: JwtToken;
    isLoggedIn: boolean;
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
    },
    receivedToken: (token: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'RECEIVE_TOKEN', token });
    }
};


const unloadedState: LoginState = { isLoggedIn: false };

export const reducer: Reducer<LoginState> = (state: LoginState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'RECEIVE_TOKEN':
            const token = new JwtToken(action.token);
            console.log(token);
            return Object.assign({}, state, {
                token,
                isLoggedIn: !token.IsGuest
            });
        case 'REQUEST_TOKEN':
            return Object.assign({}, state);
        default:
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
