import React, { Component } from 'react';

export default class RegisterProviderComponent extends Component {
    render() {
        return (
            <form action="/registerproviderpost" method="post">
                <input type="text" name="FullName"></input>
                <input type="text" name="Specialty"></input>
            </form>
        );
    }
}