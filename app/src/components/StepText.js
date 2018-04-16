import React, { Component } from 'react';
import PropTypes from 'prop-types';

import Grid from 'material-ui/Grid';
import TextField from 'material-ui/TextField';
import { withStyles } from 'material-ui/styles';
import Button from 'material-ui/Button';

const styles = theme => ({
    container: {
        display: 'flex',
        flexWrap: 'wrap',
    },
    textField: {
        margin: theme.spacing.unit        
    }
});

class StepText extends Component {

    constructor(props) {
        super(props);

        this.state = {
            id: "",
            text: ""
        };
    }

    static propTypes = {
        classes: PropTypes.object.isRequired,
    };

    handleChange = propertyName => event => {
        this.setState({
          [propertyName]: event.target.value,
        });
    };

    render() {

        const { classes, nextStep } = this.props;

        return (
            <Grid container justify="center">
                <Grid item xs={8} className={classes.container}>
                    <TextField
                        id="id-input"
                        label="Id"
                        margin="normal"
                        fullWidth
                        value={this.state.id}
                        onChange={this.handleChange('id')}
                    />
                </Grid>
                <Grid item xs={8} className={classes.container}>
                    <TextField
                        id="text-input"
                        label="Json"
                        multiline
                        rows="10"
                        defaultValue=""
                        margin="normal"
                        fullWidth
                        value={this.state.text}
                        onChange={this.handleChange('text')}
                    />
                </Grid>
                <Grid item xs={8} className={classes.container}>
                    <Button variant="raised" size="large" color="primary" onClick={() => nextStep(this.state)}>
                        Next
                    </Button>
                </Grid>
            </Grid>
        );
    }
}

export default withStyles(styles)(StepText);