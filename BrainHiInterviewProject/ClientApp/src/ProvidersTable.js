import React, { Component } from 'react';
import { ProviderTableComponent } from './ProviderTableComponent';

export class ProvidersTable extends Component {
    constructor(props) {
        super(props);
    }

    renderSingleRow(provider, index) {
        return (
            <tr key={index}>
                <td>
                    <ProviderTableComponent
                        id={provider.id}
                        name={provider.name}
                        specialty={provider.specialty} />
                </td>
            </tr>
        );
    }

    renderTableElements() {
        return this.props.providers.map((provider, index) => {
            return this.renderSingleRow(provider, index);
        });
    }

    render() {
        return (
            <table>
                <tbody>
                    {this.renderTableElements()}
                </tbody>
            </table>
        );
    }
}