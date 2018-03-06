import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import NewGame from './components/NewGame';

export const routes = <Layout>
    <Route exact path='/' component={ NewGame } />
</Layout>;
