import React from 'react';

import Grid from 'material-ui/Grid';

import InputText from './InputText';
import DiffResult from './DiffResult';
import Button from 'material-ui/Button';

const Diff = ({ diff }) => {
    return (
        <div>
            <Grid container spacing={16} justify="center">
                <Grid item xs={12} sm={12} md={6}>
                    <InputText label="Left Text" />
                </Grid>
                <Grid item xs={12} sm={12} md={6}>
                    <InputText label="Right Text"/>
                </Grid>
            </Grid>
            <DiffResult result={diff.diff} />
            <Grid container spacing={16} justify="left">
                <Grid item xs={12} sm={12} md={6}>
                    <Button variant="raised" color="primary" >
                        Compare Texts
                    </Button>
                </Grid>
            </Grid>
        </div>
    );
}

export default Diff;