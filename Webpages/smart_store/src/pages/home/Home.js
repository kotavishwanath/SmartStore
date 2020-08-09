import React, {useEffect, useReducer} from 'react';
import {useSelector} from 'react-redux';
import {Col, message, Row} from 'antd';
import {LoadingOutlined} from '@ant-design/icons';
import axios from 'axios';
import ProductCard from '../../components/product_card/ProductCard';
import './Home.css';

const initialState = {
  products: [],
  viewedProducts: [],
  loading: true,
};

const reducer = (state, action) => {
  switch (action.type) {
    case 'update-products-data':
      return {
        products: action.payload.products,
        viewedProducts: action.payload.viewedProducts,
        loading: false,
      };
    default:
      return state;
  }
};

const Home = () => {

  const [state, dispatch] = useReducer(reducer, initialState);
  const userData = useSelector(state => state.userData);

  useEffect(() => {
    getProducts();
  }, []);

  const getProducts = () => {
    const userId = userData.id;
    axios.get(`/user/getuserproducts/${userId}`).
        then(response => {
          if (response.status === 200) {
            const data = response.data;
            setTimeout(() => {
              dispatch({
                type: 'update-products-data',
                payload: {
                  products: data.productResponse,
                  viewedProducts: data.userViewedProduct,
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
        }).catch(() => {
      message.info('Server error. Refresh the page');
      dispatch({
        type: 'change-loading-status',
        payload: false,
      });
    });
  };

  const {products, viewedProducts, loading} = state;

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
              <>
                {
                  viewedProducts.length > 0 && (
                      <Row className="products-list-section">
                        <Col span={24}>
                          <p className="sub-title">For You</p>
                          <h2 className="title">Viewed Products :</h2>
                        </Col>
                        <Col span={24} className="mt-10">
                          <Row gutter={16}>
                            {
                              viewedProducts.map(product => (
                                  <Col
                                      xs={24}
                                      sm={12}
                                      md={8}
                                      lg={6}
                                      key={product.productId}
                                  >
                                    <ProductCard data={product}/>
                                  </Col>
                              ))
                            }
                          </Row>
                        </Col>
                      </Row>
                  )
                }
                {
                  products.length > 0 ? (
                      <Row className="products-list-section">
                        <Col span={24}>
                          <p className="sub-title">For You</p>
                          <h2 className="title">All Products :</h2>
                        </Col>
                        <Col span={24} className="mt-10">
                          <Row gutter={16}>
                            {
                              products.map(product => (
                                  <Col
                                      xs={24}
                                      sm={12}
                                      md={8}
                                      lg={6}
                                      key={product.productId}
                                  >
                                    <ProductCard data={product}/>
                                  </Col>
                              ))
                            }
                          </Row>
                        </Col>
                      </Row>
                  ) : <p>No products found</p>
                }
              </>
          )
        }
      </div>
  );
};

export default Home;
