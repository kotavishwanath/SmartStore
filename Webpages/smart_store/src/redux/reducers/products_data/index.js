const INITIAL_STATE = {
  products: [],
  viewedProducts: [],
  searchedProducts: [],
  isSearched: false,
  loading: true,
};

export default (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case 'update-products-data':
      return {
        ...state,
        products: action.payload.products,
        viewedProducts: action.payload.viewedProducts,
        loading: false,
      };
    case 'update-search-products':
      return {
        ...state,
        searchedProducts: action.payload,
        isSearched: true,
      };
    case 'remove-search-products':
      return {
        ...state,
        searchedProducts: [],
        isSearched: false,
      };
    case 'change-loading-status':
      return {
        ...state,
        loading: action.payload,
      };
    default:
      return state;
  }
}

