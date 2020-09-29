import React from 'react';
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import BookAppointmentComponent from './BookAppointmentComponent';

it('renders without crashing', async () => {
    const div = document.createElement('div');
    ReactDOM.render(
        <MemoryRouter>
            <BookAppointmentComponent providerId={0} />
        </MemoryRouter>, div);
    await new Promise(resolve => setTimeout(resolve, 1000));
});
