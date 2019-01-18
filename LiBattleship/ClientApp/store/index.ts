import * as Login from './Login';
import * as NewGame from './NewGame';
import * as Game from './Game';
import * as SignalR from './SignalR';
import * as GameList from './GameList';

// The top-level state object
export interface ApplicationState {
    login: Login.LoginState;
    newGame: NewGame.NewGameState;
    game: Game.GameState;
    signalR: SignalR.SignalRState;
    gameList: GameList.GameListState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    login: Login.reducer,
    newGame: NewGame.reducer,
    game: Game.reducer,
    signalR: SignalR.reducer,
    gameList: GameList.reducer,
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
