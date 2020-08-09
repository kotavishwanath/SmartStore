import {combineReducers} from 'redux';
import userDataReducer from './user_data/index';

export default combineReducers({
  userData: userDataReducer,
});
