import {
    ADD_LEFT,
    ADD_RIGHT,
    SHOW_DIFFERENCE
} from '../actions/actions'

function diffReducer(state = { left: {}, right: {}, diff: { leftResult: [], rightResult: [] } }, action){
    switch (action.type) {
        case ADD_LEFT:
            return {
                ...state,
                left: action.data
            };
        case ADD_RIGHT:
            return {
                ...state,
                right: action.data
            }
        case SHOW_DIFFERENCE:
            return {
                ...state,
                diff: action.data
            }
        default:
            return state;
    }
}

export default diffReducer;