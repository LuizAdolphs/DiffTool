import { push } from 'react-router-redux';

export const ADD_LEFT = 'ADD_LEFT';
export const ADD_RIGHT = 'ADD_RIGHT';
export const SHOW_DIFFERENCE = 'SHOW_DIFFERENCE';

export function addLeft(data) {
    return { type: ADD_LEFT, data };
}
export function addRight(data) {
    return { type: ADD_RIGHT, data };
}
export function showDifference(data) {
    return { type: SHOW_DIFFERENCE, data };
}

//async function for persistance

const url = "http://localhost:5000";

export function addLeftAsync(data) {
    return function(dispatch) {
        fetch(`${url}/api/v1/diff/${data.id}/left/`, {
            method: 'post',
            mode: 'cors',
            body: btoa(data.text)
        }).then(function(response) {
            dispatch(addLeft(data));
        }).then(function() {
            dispatch(push('/steps/right'));
        });
    }
}

export function addRightAsync(data) {
    return function(dispatch) {
        fetch(`${url}/api/v1/diff/${data.id}/right/`, {
            method: 'post',
            mode: 'cors',
            body: btoa(data.text),
            headers: new Headers({
                'Content-Type': 'text/plain'
            })
        }).then(function(response) {
            dispatch(addRight(data));
        }).then(function(){
            dispatch(showDifferenceAsync(data.id));
        });
    }
}

export function showDifferenceAsync(id) {
    return function(dispatch) {
        fetch(`${url}/api/v1/diff/${id}`, {
            method: 'get',
            mode: 'cors'
        }).then(function(response) {
            return response.json();
        }).then(function (json) {
            dispatch(showDifference(json));
        }).then(function() {
            dispatch(push('/steps/diffresult'));
        });
    }
}