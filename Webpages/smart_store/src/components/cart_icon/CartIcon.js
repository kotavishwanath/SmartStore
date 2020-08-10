import React from 'react';
import {useDispatch, useSelector} from 'react-redux';
import {Badge} from 'antd';
import {ShoppingCartOutlined} from '@ant-design/icons';
import CartModal from '../cart_modal/CartModal';
import {handleCartModalVisibility} from '../../redux/actions/cart_data_actions';
import {getCartDataReselector} from '../../redux/selectors';

const CartIcon = () => {
  const {productsList} = useSelector(getCartDataReselector);
  const dispatch = useDispatch();

  const showModal = () => dispatch(handleCartModalVisibility(true));
  const cartProductsCount = productsList.length;

  return (
      <>
        <Badge count={cartProductsCount}>
          <ShoppingCartOutlined
              className="cart-icon"
              onClick={showModal}
          />
        </Badge>
        <CartModal/>
      </>
  );
};

export default CartIcon;
