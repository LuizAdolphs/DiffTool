import React from 'react';

import Grid from 'material-ui/Grid';

import InputText from './InputText';

const Diff = (props) => {
    return (
        <Grid container spacing={24}>
            <Grid item xs={12} sm={12} md={6}>
                <InputText />
            </Grid>
            <Grid item xs={12} sm={12} md={6}>
                <InputText />
            </Grid>
        </Grid>
    );
}

export default Diff;