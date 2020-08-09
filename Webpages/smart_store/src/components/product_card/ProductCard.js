import React from 'react';
import {Link} from 'react-router-dom';
import './ProductCard.css';

const ProductCard = ({data}) => {
  const {productId, productName, productPrice, productImagePath, categoryId} = data;
  const productImage = require('../../assets' + productImagePath);

  return (
      <div className="product-card">
        <Link
            to={`/home/product/categoryId=${categoryId}/productId=${productId}`}
            className="product-image"
        >
          <img src={productImage} alt={productName}/>
        </Link>
        <div className="product-details">
          <p>Available<span>(In Stock)</span></p>
          <h4>{productName}</h4>
          <div className="product-price">£{productPrice} <span>{productPrice + 5}</span></div>
        </div>
      </div>
  );
};

export default ProductCard;
