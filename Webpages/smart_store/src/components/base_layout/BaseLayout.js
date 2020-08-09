import React from 'react';
import {Link, Route, Switch, useRouteMatch} from 'react-router-dom';
import {Col, Dropdown, Menu, Row} from 'antd';
import Home from '../../pages/home/Home';
import ProductDetailed from '../../pages/product_detailed/ProductDetailed';
import logo from '../../assets/images/logo.png';
import user from '../../assets/images/user.png';

const BaseLayout = () => {

  const {path} = useRouteMatch();

  console.log(path);

  const menu = (
      <Menu>
        <Menu.Item>
          <a
              target="_blank"
              rel="noopener noreferrer"
              href="http://www.alipay.com/"
          >
            Log out
          </a>
        </Menu.Item>
      </Menu>
  );

  return (
      <Row>
        <Col span={24}>
          <div className="navigation-header">
            <Row justify="center" align="middle">
              <Col span={12}>
                <Link to="/">
                  <img src={logo} alt="logo" width={50}/>
                </Link>
              </Col>
              <Col span={12} className="text-right">
                <Dropdown overlay={menu}>
                  <img src={user} alt="user" width={30}/>
                </Dropdown>
              </Col>
            </Row>
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
