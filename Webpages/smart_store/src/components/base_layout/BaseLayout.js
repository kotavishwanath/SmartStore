import React from 'react';
import {Link, Route, Switch} from 'react-router-dom';
import {useDispatch} from 'react-redux';
import {Avatar, Col, Dropdown, Input, Menu, Row} from 'antd';
import {UserOutlined} from '@ant-design/icons';
import Home from '../../pages/home/Home';
import ProductDetailed from '../../pages/product_detailed/ProductDetailed';
import {searchProducts} from '../../redux/actions/products_data_actions';
import logo from '../../assets/images/logo.png';
import CartIcon from '../cart_icon/CartIcon';

const {Search} = Input;

const BaseLayout = () => {

  const dispatch = useDispatch();

  const handleLogOut = () => {
    localStorage.clear();
    window.location.reload();
  };

  const handleSearch = value => {
    dispatch(searchProducts(value));
  };

  const menu = (
      <Menu>
        <Menu.Item onClick={handleLogOut}>Log out</Menu.Item>
      </Menu>
  );

  return (
      <Row>
        <Col span={24}>
          <div className="navigation-header">
            <div className="navigation-header-left">
              <Link to="/home/">
                <img src={logo} alt="logo" width={50}/>
              </Link>
              <Search
                  placeholder="Enter search text"
                  size="large"
                  allowClear
                  className="search-input"
                  onSearch={handleSearch}
              />
            </div>
            <div className="navigation-header-right">
              <CartIcon/>
              <Dropdown overlay={menu}>
                <Avatar icon={<UserOutlined/>} className="user-icon"/>
              </Dropdown>
            </div>
          </div>
        </Col>
        <Col span={24}>
          <Switch>
            <Route exact path="/home/">
              <Home/>
            </Route>
            <Route path="/home/product/categoryId=:categoryId/productId=:productId">
              <ProductDetailed/>
            </Route>
          </Switch>
        </Col>
      </Row>
  );
};

export default BaseLayout;
