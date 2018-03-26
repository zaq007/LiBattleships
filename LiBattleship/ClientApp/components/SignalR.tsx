import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as SignalRStore from '../store/SignalR';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameField from './GameField';
import { HubConnection } from '@aspnet/signalr';

interface ComponentParams {
    hub: string;
}

type SignalRProps =
    SignalRStore.SignalRState
    & typeof SignalRStore.actionCreators
    & ComponentParams;

class SignalR extends React.Component<SignalRProps, {}> {
    connection: HubConnection;

    componentWillMount() {
        this.connection = new HubConnection("http://localhost:61930/hubs/battleships");
        this.connection.on('setCount', this.props.setStatus);
        this.startConnection();
        this.connection.onclose((e) => {
            this.props.onDisconnected(e);
            this.startConnection();
        });

    }

    render() {
        return <div>
            <span>{this.props.connected ? 'connected' : 'disconnected'}</span>
            <span hidden={!this.props.connected}>Online: {this.props.onlineCount}</span>
        </div>;
    }

    startConnection() {
        this.connection.start().then((data) => this.props.onConnected(this.connection.send.bind(this.connection)));
    }
}

export default connect(
    (state: ApplicationState) => state.signalR, // Selects which state properties are merged into the component's props
    SignalRStore.actionCreators                 // Selects which action creators are merged into the component's props
)(SignalR);
