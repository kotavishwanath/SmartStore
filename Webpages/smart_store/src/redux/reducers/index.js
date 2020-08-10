import {combineReducers} from 'redux';
import productsDataReducer from './products_data/index';
import userDataReducer from './user_data/index';
import cartDataReducer from './cart_data/index';

export default combineReducers({
  productsData: productsDataReducer,
  userData: userDataReducer,
  cartData: cartDataReducer,
});
