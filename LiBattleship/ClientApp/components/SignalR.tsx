import * as React from 'react';
import * as SignalRStore from '../store/SignalR';
import * as GameListStore from '../store/GameList';
import * as GameStore from '../store/Game';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import { HubConnection, HubConnectionBuilder, HttpTransportType } from '@aspnet/signalr';
import { AuthService } from '../services/AuthService';

interface ComponentParams {
    hub: string;
}

type SignalRProps =
    SignalRStore.SignalRState
    & typeof SignalRStore.actionCreators
    & typeof GameListStore.actionCreators
    & typeof GameStore.actionCreators
    & ComponentParams;

class SignalR extends React.Component<SignalRProps, {}> {
    connection: HubConnection = new HubConnectionBuilder().withUrl("http://localhost:4761/hubs/battleships", {
        accessTokenFactory: () => AuthService.getToken(),
    }).build();

    componentWillMount() {
        this.connection.on('setCount', this.props.setStatus);
        this.connection.on('setGameList', this.props.setGameList);
        this.connection.on('gameCreated', this.props.gameCreated);
        this.connection.on('setGameState', this.props.setGameState);
        this.startConnection();
    }

    render() {
        return <div>
            <span>{this.props.connected ? 'connected' : 'disconnected'}</span>
            <span hidden={!this.props.connected}>Online: {this.props.onlineCount}</span>
        </div>;
    }

    startConnection() {
        this.connection.start().then((data) => this.props.onConnected());
    }
}

export default connect(
    (state: ApplicationState) => state.signalR,
    Object.assign({}, SignalRStore.actionCreators, GameListStore.actionCreators, GameStore.actionCreators)
)(SignalR);
