import axios from 'axios';
import {message} from 'antd';

export const getProducts = () => {
  return async (dispatch, getState) => {
    const userId = getState().userData.id;
    axios.get(`/user/getuserproducts/${userId}`).
        then(response => {
          if (response.status === 200) {
            const {productResponse, userViewedProduct} = response.data;
            setTimeout(() => {
              dispatch({
                type: 'update-products-data',
                payload: {
                  products: productResponse,
                  viewedProducts: userViewedProduct,
                },
              });
            }, 1000);
          } else {
            message.info('No products found');
            dispatch({
              type: 'change-loading-status',
              payload: false,
            });
          }
        }).
        catch(() => {
          message.info('Server error. Refresh the page');
          dispatch({
            type: 'change-loading-status',
            payload: false,
          });
        });
  };
};

export const searchProducts = value => {
  return async (dispatch, getState) => {
    const {products} = getState().productsData;

    if (value !== '') {
      const searchedProducts = products.filter(product => {
        const {productName} = product;
        if (productName.includes(value)) return product;
      });

      dispatch({
        type: 'update-search-products',
        payload: searchedProducts,
      });
    } else {
      dispatch({
        type: 'remove-search-products',
      });
    }
  };
};
