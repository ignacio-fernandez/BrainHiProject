import React from 'react';
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import ProvidersContainer from './ProvidersContainer';

it('renders without crashing', async () => {
    const div = document.createElement('div');
    ReactDOM.render(
        <MemoryRouter>
            <ProvidersContainer providers={[]} />
        </MemoryRouter>, div);
    await new Promise(resolve => setTimeout(resolve, 1000));
});

it('renders without crashing when providers present', async () => {
    const div = document.createElement('div');
    ReactDOM.render(
        <MemoryRouter>
            <ProvidersContainer providers={[{
                id: '0',
                name: 'foo',
                specialty: 'bah'
            }, {
                id: '1',
                name: 'foo',
                specialty: 'bah'
                }, {
                id: '2',
                name: 'foo',
                specialty: 'bah'
                }]} />
        </MemoryRouter>, div);
    await new Promise(resolve => setTimeout(resolve, 1000));
});
