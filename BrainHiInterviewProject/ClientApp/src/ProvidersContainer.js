import React, { Component } from 'react';
import { ProvidersTable } from './ProvidersTable';
import { SearchProvidersComponent } from './SearchProvidersComponent';

export default class ProvidersContainer extends Component {
    constructor(props) {
        super(props);

        this.state = {
            providers: props.providers
        };
    }
    
    updateProviders = (providers) => {
        this.setState({ providers: providers });
    }

    registerProvider = async () => {
        await fetch('registerprovider');
    }

    render() {
        return (
            <div>
                <SearchProvidersComponent updateProviders={this.updateProviders} />
                <ProvidersTable
                    providers={this.state.providers} />
                <button id="register-provider-btn" onClick={this.registerProvider}>Register New Provider</button>
            </div>
        );
    }
}