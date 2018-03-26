import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';
import Login from './Login';
import SignalR from './SignalR';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <nav className="navbar navbar-default">
            <div className="container-fluid">
                <div className="navbar-header">
                    <Link className='navbar-brand' to={'/'}>LiBattleship</Link>
                </div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink exact to={'/'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/newGame'} activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> New Game
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/fetchdata'} activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                        </li>
                    </ul>
                    <SignalR />
                    <Login />
                        </div>
                  </div>
                </nav>;
    }
}
