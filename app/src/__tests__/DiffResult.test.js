import React from 'react';
import ReactDOM from 'react-dom';
import DiffResult from '../components/DiffResult';

it('renders DiffResult without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<DiffResult 
      result={{ 
        leftResult: [], 
        rightResult: [] 
    }}
    />, div);
    ReactDOM.unmountComponentAtNode(div);
});
