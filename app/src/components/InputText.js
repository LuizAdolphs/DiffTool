import React from 'react';

import Paper from 'material-ui/Paper';
import TextField from 'material-ui/TextField';
import { withStyles } from 'material-ui/styles';

const styles = theme => ({
    textField: {
    },
    paper: {
        paddingLeft: theme.spacing.unit * 2,
        paddingRight: theme.spacing.unit * 2
    }
  });

const InputText = ({classes, label}) => {
    return (
    <Paper className={classes.paper} >
        <TextField
            id="multiline-flexible"
            multiline
            rowsMax="255"
            margin="dense"
            fullWidth={true}
            label={label}
            className={classes.textField}
        />   
    </Paper>);
}

export default withStyles(styles)(InputText);
