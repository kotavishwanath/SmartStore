import React, {useEffect} from 'react';
import {useDispatch, useSelector} from 'react-redux';
import {Col, Empty, Row} from 'antd';
import {LoadingOutlined} from '@ant-design/icons';
import ProductCard from '../../components/product_card/ProductCard';
import {getProducts} from '../../redux/actions/products_data_actions';
import {getProductsDataReselector} from '../../redux/selectors';
import './Home.css';

const Home = () => {
  const {products, viewedProducts, searchedProducts, isSearched, loading} = useSelector(
      getProductsDataReselector);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getProducts());
  }, []);

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
              isSearched ? (
                  <Row className="products-list-section">
                    <Col span={24}>
                      <p className="sub-title">Your</p>
                      <h2 className="title">Searched Products :</h2>
                    </Col>
                    <Col span={24} className="mt-10">
                      {
                        searchedProducts.length > 0 ? (
                            <Row gutter={16}>
                              {
                                searchedProducts.map(product => (
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
                        ) : <Empty className="mt-50" description={<span>No products found</span>} />
                      }
                    </Col>
                  </Row>
              ) : (
                  <>
                    {
                      viewedProducts.length > 0 && (
                          <Row className="products-list-section">
                            <Col span={24}>
                              {/* <p className="sub-title">For You</p> */}
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
                              {/* <p className="sub-title">For You</p> */}
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
          )
        }
      </div>
  );
};

export default Home;
