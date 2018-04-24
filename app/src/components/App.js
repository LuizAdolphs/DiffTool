import React, { Component } from 'react';
import PropTypes from 'prop-types';

import { Provider, connect } from 'react-redux';

import { withStyles } from 'material-ui/styles';

import Diff from './Diff';
import Steps from './Steps';

import { addLeftAsync, addRightAsync } from '../actions/actions';

import { Route, Redirect } from 'react-router-dom';

import { ConnectedRouter } from 'react-router-redux';

const styles = theme => ({
    root: {
        flexGrow: 1,
    }
});

export class DiffApp extends Component {

    static propTypes = {
        classes: PropTypes.object.isRequired,
        store: PropTypes.object.isRequired
    };

    render() {
        const { classes, store, history, values, actions } = this.props;

        return (
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <div className={classes.root}>
                        <Route exact path="/" render={() => <Redirect to="/steps/left"/>} />
                        <Route path="/diff" render={(routeProps) => <Diff {...routeProps} {...values} {...actions} />} />
                        <Route path="/steps" render={(routeProps) => <Steps {...routeProps} {...values} {...actions} />} />
                    </div>
                </ConnectedRouter>
            </Provider>
        );
    }
}

function mapStateToProps(state) {
    return {
        values: state
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: {
            addLeft: (data) => dispatch(addLeftAsync(data)),
            addRight: (data) => dispatch(addRightAsync(data)),
        }
    };
}

const App = connect(
    mapStateToProps,
    mapDispatchToProps
)(DiffApp);

export default withStyles(styles)(App);
