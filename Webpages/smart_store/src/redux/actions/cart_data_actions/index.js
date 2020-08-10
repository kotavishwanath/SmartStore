import {store} from '../../store';

export const addToCart = data => {
  let {productsList} = store.getState().cartData;
  productsList.push(data);
  localStorage.setItem('cartProducts', JSON.stringify(productsList));
  return {
    type: 'update-cart-products-list',
    payload: productsList,
  };
};

export const removeFromCart = productId => {
  const {productsList} = store.getState().cartData;
  const modifiedProductList = productsList.filter(product => product.productId !== productId && product);
  localStorage.setItem('cartProducts', JSON.stringify(modifiedProductList));
  return {
    type: 'update-cart-products-list',
    payload: modifiedProductList,
  };
};

export const handleCartModalVisibility = visibility => {
  return {
    type: 'update-cart-modal-visibility',
    payload: visibility,
  };
};
