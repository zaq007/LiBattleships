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
}

interface DisconnectedAction {
    type: 'DISCONNECTED';
}

interface SetStatusAction {
    type: 'SET_STATUS';
    onlineCount: number;
}

type KnownAction = ConnectedAction | DisconnectedAction | SetStatusAction;

export const actionCreators = {
    onConnected: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CONNECTED' });
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
            return Object.assign({}, state, { connected: true });
        case 'DISCONNECTED':
            return Object.assign({}, state, { connected: false });
        case 'SET_STATUS':
            return Object.assign({}, state, { onlineCount: action.onlineCount });
        default:
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
