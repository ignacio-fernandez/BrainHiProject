import React, { Component } from 'react';

export class SearchProvidersComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            name: '',
            specialty: ''
        };
    }

    handleInput = (e) => {
        this.setState({ [e.target.name]: e.target.value })
    }

    searchProviders = async () => {
        const response = await fetch(`searchproviders?providerName${this.state.name}?providerSpecialty=${this.state.specialty}`);
        const providers = await response.json();
        this.props.updateProviders(providers);
    }

    render() {
        return (
            <div>
                <input type="text" name="name" onChange={(e) => this.handleInput(e)}></input>
                <input type="text" name="specialty" onChange={(e) => this.handleInput(e)}></input>
                <button id="search-providers-btn" onClick={this.searchProviders}>Search</button>
            </div>
        );
    }
}