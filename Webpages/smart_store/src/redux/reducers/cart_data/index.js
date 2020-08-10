const productsList = localStorage.getItem('cartProducts');

const INITIAL_STATE = {
  productsList: productsList === null ? [] : JSON.parse(productsList),
  showCartModal: false,
};

export default (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case 'update-cart-products-list':
      return {
        ...state,
        productsList: action.payload,
      };
    case 'update-cart-modal-visibility':
      return {
        ...state,
        showCartModal: action.payload,
      };
    default:
      return state;
  }
}
