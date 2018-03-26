import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface SignalRState {
    connected: boolean;
    onlineCount: number;
    send: Function;
}

interface ConnectedAction {
    type: 'CONNECTED';
    send: Function;
}

interface GotDataAction {
    type: 'GOT_DATA';
}

interface DisconnectedAction {
    type: 'DISCONNECTED';
}

interface SetStatusAction {
    type: 'SET_STATUS';
    onlineCount: number;
}

type KnownAction = ConnectedAction | GotDataAction | DisconnectedAction | SetStatusAction;

export const actionCreators = {
    onConnected: (send: Function): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CONNECTED', send });
    },
    onDisconnected: (e?: Error): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'DISCONNECTED' });
    },
    setStatus: (online: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_STATUS', onlineCount: online });
    },
};


const unloadedState: SignalRState = { connected: false, send: () => false, onlineCount: 0 };

export const reducer: Reducer<SignalRState> = (state: SignalRState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'CONNECTED':
            return Object.assign({}, state, { send: action.send, connected: true });
        case 'DISCONNECTED':
            return Object.assign({}, state, { connected: false });
        case 'GOT_DATA':
            return Object.assign({}, state);
        case 'SET_STATUS':
            return Object.assign({}, state, { onlineCount: action.onlineCount });
        default:
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
