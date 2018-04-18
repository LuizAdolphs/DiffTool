import React from 'react';
import ReactDOM from 'react-dom';
import StepText from '../components/StepText';

it('renders StepText without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<StepText 
      nextStep={() => {}}
      currentData={{ id: "", text: "" }}    
    />, div);
    ReactDOM.unmountComponentAtNode(div);
});
