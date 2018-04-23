import React from 'react';

import { Route, Redirect } from 'react-router-dom';

import Grid from 'material-ui/Grid';
import Stepper, { Step, StepLabel } from 'material-ui/Stepper';

import StepText from './StepText';
import DiffResult from './DiffResult';

const Steps = ({match, location, diff, addLeft, addRight}) => {

    const findActiveStep = (pathname) => {
        if (pathname == null) {
            return 0;
        }

        switch(pathname){
            case `${match.url}/diffresult`:
                return 2;
            case `${match.url}/right`:
                return 1;
            case `${match.url}/left`:
            default:
                return 0;
        }
    }

    const steps = ["Left Text", "Right Text", "Difference"];

    return (
        <div>
            <Stepper activeStep={findActiveStep(location.pathname)}>
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
                    <Route exact path={`${match.url}/left`} render={() => <StepText nextStep={addLeft} currentData={diff.left}/>} />
                    <Route exact path={`${match.url}/right`} render={() => <StepText nextStep={addRight}  currentData={diff.right}/>} />
                    <Route exact path={`${match.url}/diffresult`} render={() => <DiffResult result={diff.diff}/>} />
                </Grid>
            </Grid>
        </div>
    );
}

export default Steps;