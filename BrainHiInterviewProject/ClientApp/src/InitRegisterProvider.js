import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import RegisterProviderComponent from './RegisterProviderComponent';

const rootElement = document.getElementById('root');

ReactDOM.render(
    <BrowserRouter basename={'/registerprovider'}>
        <RegisterProviderComponent />
    </BrowserRouter>,
    rootElement);