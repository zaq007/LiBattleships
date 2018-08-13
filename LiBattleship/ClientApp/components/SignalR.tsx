import * as React from 'react';
import * as SignalRStore from '../store/SignalR';
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
    & ComponentParams;

class SignalR extends React.Component<SignalRProps, {}> {
    connection: HubConnection = new HubConnectionBuilder().withUrl("http://localhost:4761/hubs/battleships", {
        transport: HttpTransportType.LongPolling,
        accessTokenFactory: () => AuthService.getToken(),
    }).build();

    componentWillMount() {
        this.connection.on('setCount', this.props.setStatus);
        this.startConnection();
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
