import { createStore, combineReducers, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';

import createHistory from 'history/createBrowserHistory';
import { routerReducer, routerMiddleware } from 'react-router-redux';

import diffReducer from '../reducers/reducers';

export const history = createHistory();

const historyMiddleware = routerMiddleware(history);

export const diffStore = createStore(combineReducers({
    diff: diffReducer,
    router: routerReducer
  }),
  applyMiddleware(historyMiddleware,thunk)
);