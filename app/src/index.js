import React from 'react';
import ReactDOM from 'react-dom';

import App from './components/App';
import { diffStore, history } from './stores/stores'

ReactDOM.render(<App store={diffStore} history={history} />, 
    document.getElementById('root'));

