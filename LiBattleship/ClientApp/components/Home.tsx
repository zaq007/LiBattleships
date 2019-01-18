import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import GameList from './GameList';

export default class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <GameList />;
    }
}
