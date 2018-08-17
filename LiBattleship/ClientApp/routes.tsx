import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import NewGame from './components/NewGame';
import Game from './components/Game';
import JoinGame from './components/JoinGame';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/newGame' component={NewGame} />
    <Route exact path='/join/:id' component={JoinGame} />
    <Route exact path='/game/:id' component={Game} />
</Layout>;
