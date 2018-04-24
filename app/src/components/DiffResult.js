import React, { Component } from 'react';

import Grid from 'material-ui/Grid';
import { withStyles } from 'material-ui/styles';
import Typography from 'material-ui/Typography';
import Paper from 'material-ui/Paper';

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
    },
    paper: {
        paddingLeft: theme.spacing.unit * 2,
        paddingRight: theme.spacing.unit * 2
    }
});

class DiffResult extends Component {

    render() {

        const { result, classes } = this.props;

        return (
            <Grid container spacing={16} justify="center">
                <Grid item xs={6} className={classes.container}>
                    <Paper className={classes.paper}>
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
                    </Paper>
                </Grid>
                <Grid item xs={6} className={classes.container}>
                    <Paper className={classes.paper}>
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
                    </Paper>
                </Grid>
            </Grid>
        );
    }
};

export default withStyles(styles)(DiffResult);