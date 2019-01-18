import { AppThunkAction } from ".";
import { push } from 'react-router-redux';
import { LocationDescriptor } from "history";

export const actionCreators = {
    push: (path: LocationDescriptor): AppThunkAction<any> => (dispatch, getState) => {
        dispatch(push(path));
    },
};