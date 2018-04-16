import React, { Component } from 'react';
import PropTypes from 'prop-types';

import { Provider, connect } from 'react-redux';

import Grid from 'material-ui/Grid';
import { withStyles } from 'material-ui/styles';
import Stepper, { Step, StepLabel } from 'material-ui/Stepper';

import StepText from './StepText';
import DiffResult from './DiffResult';

import { addLeftAsync, addRightAsync } from '../actions/actions';

import {
    Route
  } from 'react-router-dom'

import { ConnectedRouter } from 'react-router-redux';

const styles = theme => ({
    root: {
        flexGrow: 1,
    }
});

class DiffApp extends Component {
    static propTypes = {
        classes: PropTypes.object.isRequired,
        store: PropTypes.object.isRequired
    };

    findActiveStep = (location) => {
        if (location == null) {
            return 0;
        }

        switch(location.pathname){
            case "/diff":
                return 2;
            case "/right":
                return 1;
            case "/left":
            default:
                return 0;
        }
    }

    render() {
        const { classes, store, addLeft, addRight, history, values } = this.props;

        const steps = ["Left Text", "Right Text", "Difference"];

        return (
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <div className={classes.root}>
                        <Stepper activeStep={this.findActiveStep(values.router.location)}>
                            {steps.map((label, index) => {
                            
                                return (
                                    <Step key={label}>
                                        <StepLabel>{label}</StepLabel>
                                    </Step>
                                );
                            })}
                        </Stepper>
                        <Grid container justify="center">
                            <Grid item xs={12}>
                                <Route exact path="/(left|)/" render={(props) => <StepText nextStep={addLeft}/>} />
                                <Route exact path="/right" render={(props) => <StepText nextStep={addRight}/>} />
                                <Route exact path="/diff" render={(props) => <DiffResult result={values.diff.diff}/>} />
                            </Grid>
                        </Grid>
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
        addLeft: (data) => dispatch(addLeftAsync(data)),
        addRight: (data) => dispatch(addRightAsync(data)),
    };
}

const App = connect(
    mapStateToProps,
    mapDispatchToProps
)(DiffApp);

export default withStyles(styles)(App);
