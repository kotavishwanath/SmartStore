import React from 'react';
import {useDispatch, useSelector} from 'react-redux';
import {Button, Col, Row} from 'antd';
import {
  addToCart,
  handleCartModalVisibility,
} from '../../redux/actions/cart_data_actions';
import {getCartDataReselector} from '../../redux/selectors';
import {search} from '../../Util';
import './ProductDetailedCard.css';

const ProductDetailedCard = ({data}) => {

  const {productsList} = useSelector(getCartDataReselector);
  const dispatch = useDispatch();

  const {productId, productName, productPrice, productImagePath, productDescription} = data;

  const productImage = require('../../assets' + productImagePath);

  const isAddedToCart = search(productId, productsList);

  const handleAddToCart = () => {
    isAddedToCart ?
        dispatch(handleCartModalVisibility(true)) :
        dispatch(addToCart(data));
  };

  return (
      <div className="product-detailed-section">
        <Row>
          <Col xs={24} md={10} className="text-center">
            <img
                src={productImage}
                alt={productName}
                className="product-image"
            />
          </Col>
          <Col xs={24} md={14}>
            <div className="pds-right">
              <h2>{productName}</h2>
              <div className="no-stock">
                <p className="pd-no">Product No.<span>{productId}</span></p>
                <p className="stock-qty">Available<span>(Instock)</span></p>
              </div>
              <p className="product-description">{productDescription}</p>
              <div className="product-purchase-section">
                <ul>
                  <li>
                    <div className="main-price color-discount">Discount
                      Price<span> £{productPrice}</span></div>
                  </li>
                  <li>
                    <div className="main-price mrp-price">MRP
                      Price<span> £{productPrice + 5}</span></div>
                  </li>
                </ul>
                <ul className="order-crt-share">
                  <li>
                    <Button type="primary" danger
                            onClick={handleAddToCart}>{isAddedToCart ?
                        'Open Cart' :
                        'Add to Cart'}</Button>
                  </li>
                  <li>
                    <Button type="dashed" danger>Buy Now</Button>
                  </li>
                </ul>
              </div>
            </div>
          </Col>
        </Row>
      </div>
  );
};

export default ProductDetailedCard;
