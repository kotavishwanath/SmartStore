import React, {useEffect, useReducer} from 'react';
import {useParams} from 'react-router';
import {Col, message, Row} from 'antd';
import {LoadingOutlined} from '@ant-design/icons';
import './ProductDetailed.css';
import axios from 'axios';
import {useSelector} from 'react-redux';
import ProductDetailedCard
  from '../../components/product_detailed_card/ProductDetailedCard';
import ProductCard from '../../components/product_card/ProductCard';
import { getUserDataReselector } from '../../redux/selectors';

const initialState = {
  productData: null,
  relatedProducts: [],
  loading: false,
};

const reducer = (state, action) => {
  switch (action.type) {
    case 'update-product-data':
      return {
        ...state,
        productData: action.payload.productData,
        relatedProducts: action.payload.relatedProducts,
        loading: false,
      };
    case 'change-loading-status':
      return {
        ...state,
        loading: action.payload,
      };
    default:
      return state;
  }
};

const ProductDetailed = () => {

  const [state, dispatch] = useReducer(reducer, initialState);
  const {productId, categoryId} = useParams();
  const userData = useSelector(getUserDataReselector);
  console.log(userData);
  useEffect(() => {
    getProductData();
  }, [productId]);

  const getProductData = () => {
    dispatch({
      type: 'change-loading-status',
      payload: true,
    });
    axios.get(`/user/getproductdetails/${productId}/${categoryId}/${userData.id}`).
        then(response => {
          if (response.status === 200) {
            const {suggestedProductResponse, ...productData} = response.data;
            setTimeout(() => {
              dispatch({
                type: 'update-product-data',
                payload: {
                  productData,
                  relatedProducts: suggestedProductResponse,
                },
              });
            }, 1000);
          } else {
            message.info('Product details not found. Please check the URL.');
            dispatch({
              type: 'change-loading-status',
              payload: false,
            });
          }
        }).
        catch(() => {
          message.error('Server error. Refresh the page');
          dispatch({
            type: 'change-loading-status',
            payload: false,
          });
        });
  };

  const {productData, relatedProducts, loading} = state;

  return (
      <div className="container">
        {
          loading ? (
              <Row>
                <Col span={24} className="text-center">
                  <h3 className='mt-100'><b><LoadingOutlined
                      className="mr-10"/> Loading...</b></h3>
                </Col>
              </Row>
          ) : (
              productData === null ? (
                  <>
                  </>
              ) : (
                  <>
                    <ProductDetailedCard data={productData}/>
                    {
                      relatedProducts.length > 1 && (
                          <Row className="products-list-section">
                            <Col span={24}>
                              {/* <p className="sub-title">For You</p> */}
                              <h2 className="title">Sugested Products :</h2>
                            </Col>
                            <Col span={24} className="mt-10">
                              <Row gutter={16}>
                                {
                                  relatedProducts.map(product => {
                                    const prodId = product.productId;
                                    if (prodId != productId) return (
                                        <Col
                                            xs={24}
                                            sm={12}
                                            md={8}
                                            lg={6}
                                            key={prodId}
                                        >
                                          <ProductCard data={product}/>
                                        </Col>
                                    );
                                  })
                                }
                              </Row>
                            </Col>
                          </Row>
                      )
                    }
                  </>
              )
          )
        }
      </div>
  );
};

export default ProductDetailed;
