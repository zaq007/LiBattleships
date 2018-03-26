import * as React from 'react';
import { NavMenu } from './NavMenu';

export class Layout extends React.Component<{}, {}> {
    public render() {
        return <div>
            <NavMenu />
            <div className="container">
                <div className='row'>
                    <div className='col-sm-3'>
                        
                    </div>
                    <div className='col-sm-9'>
                        { this.props.children }
                    </div>
                    <div className='col-sm-3'>

                    </div>
                 </div>
            </div>
        </div>;
    }
}
