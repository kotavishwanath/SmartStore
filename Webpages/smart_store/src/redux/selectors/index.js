import {createSelector}  from 'reselect';

const getProductsDataSelector = state => state.productsData;
const getUserDataSelector = state => state.userData;
const getUserCartDataSelector = state => state.cartData;


export const getProductsDataReselector = createSelector(getProductsDataSelector, productsData => productsData);
export const getUserDataReselector = createSelector(getUserDataSelector, userData => userData);
export const getCartDataReselector = createSelector(getUserCartDataSelector, cartData => cartData);
