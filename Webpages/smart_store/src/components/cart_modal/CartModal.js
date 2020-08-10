import React from 'react';
import {useDispatch, useSelector} from 'react-redux';
import {Col, Divider, Empty, Modal, Row} from 'antd';
import {CloseOutlined} from '@ant-design/icons';
import {
  handleCartModalVisibility,
  removeFromCart,
} from '../../redux/actions/cart_data_actions';
import {getCartDataReselector} from '../../redux/selectors';
import './CartModal.css';

const CartModal = () => {
  const {productsList, showCartModal} = useSelector(getCartDataReselector);
  const dispatch = useDispatch();

  const handleOk = () => {
    dispatch(handleCartModalVisibility(false));
  };

  const handleCancel = () => {
    dispatch(handleCartModalVisibility(false));
  };
  const productsLength = productsList.length;
  const productsPrices = productsList.map(prod => parseInt(prod.productPrice, 10));
  const totalPrice = productsLength > 0 ? productsPrices.reduce((total, acc) => total + acc) : 0;

  return (
      <Modal
          title="My Cart"
          visible={showCartModal}
          okText="Proceed to Checkout"
          okButtonProps={{type: 'primary', danger: true}}
          onOk={handleOk}
          cancelButtonProps={{type: 'dashed', danger: true}}
          onCancel={handleCancel}
          width={500}
          className="cart-modal"
      >
        {
          productsLength > 0 ? (
              <Row>
                <Col span={24}>
                  {
                    productsList.map(product => {
                      const {productId, productName, productPrice, productImagePath} = product;
                      const productImage = require('../../assets' + productImagePath);
                      const handleRemove = () => dispatch(removeFromCart(productId));

                      return (
                          <div className="cart-list-section" key={productId}>
                            <img
                                src={productImage}
                                alt={productName}
                                className="product-image"
                            />
                            <div className="product-details">
                              <CloseOutlined
                                  className="product-remove-icon"
                                  onClick={handleRemove}
                              />
                              <h2>{productName}</h2>
                              <p>Product No: {productId}</p>
                              <div className="product-price">£{productPrice} <span>{productPrice + 5}</span></div>
                            </div>
                          </div>
                      );
                    })
                  }
                </Col>
                <Col span={24} className="mt-20">
                  <div className="ph-20">
                    <Row>
                      <Col span={12}>
                        <h4 className="gray-text">Total Saving</h4>
                      </Col>
                      <Col span={12} className="text-right">
                        <h4 className="gray-text">£{productsLength * 5}</h4>
                      </Col>
                    </Row>
                    <Row className="mt-10">
                      <Col span={12}>
                        <h2>Total</h2>
                      </Col>
                      <Col span={12} className="text-right">
                        <h2 className="orange-text">£{totalPrice}</h2>
                      </Col>
                    </Row>
                  </div>
                </Col>
              </Row>
          ) : <Empty className="mt-30 mb-30" description={<span>No products in cart</span>} />
        }
      </Modal>
  );
};

export default CartModal;
