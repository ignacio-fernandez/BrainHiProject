import React from 'react';
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import RegisterProviderComponent from './RegisterProviderComponent';

it('renders without crashing', async () => {
  const div = document.createElement('div');
  ReactDOM.render(
      <MemoryRouter>
          <RegisterProviderComponent /> 
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
});
