import React, { Component } from 'react';

import Grid from 'material-ui/Grid';
import { withStyles } from 'material-ui/styles';
import Typography from 'material-ui/Typography';

const styles = theme => ({
    container: {
        flexWrap: 'wrap',
        padding: theme.spacing.unit
    },
    lineOk: {
        background: '#98fb98'
    },
    lineNotOk: {
        background: '#FB9898'
    }
});

class DiffResult extends Component {
    


    render() {

        const { result, classes } = this.props;

        return (
            <Grid container justify="center">
                <Grid item xs={6} className={classes.container}>
                    <Typography component="h3">
                        Left Result
                    </Typography>
                    {result.leftResult.map((line, index) =>{
                        return (
                            <Typography key={index} component="p" className={line.match ? classes.lineOk : classes.lineNotOk }>
                                {line.line}
                            </Typography>
                        );    
                    })}
                </Grid>
                <Grid item xs={6} className={classes.container}>
                    <Typography component="h3">
                        Right Result
                    </Typography>
                    {result.rightResult.map((line, index) =>{
                        return (
                            <Typography key={index} component="p" className={line.match ? classes.lineOk : classes.lineNotOk }>
                                {line.line}
                            </Typography>
                        );    
                    })}
                </Grid>
            </Grid>
        );
    }
};

export default withStyles(styles)(DiffResult);