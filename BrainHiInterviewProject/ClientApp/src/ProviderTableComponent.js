import React, { Component } from 'react';

export class ProviderTableComponent extends Component {
    constructor(props) {
        super(props);
    }

    onProviderClick = async () => {
        await fetch(`bookappointment?providerId=${this.props.id}`)
    }

    render() {
        return (
            <div onClick={this.onProviderClick}>
                <div>{this.props.name}</div>
                <div>{this.props.specialty}</div>
            </div>
        );
    }
}