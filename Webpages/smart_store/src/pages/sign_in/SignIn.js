import React from 'react';
import {Link} from 'react-router-dom';
import {Button, Col, Form, Input, message, Row} from 'antd';
import {LockOutlined, UserOutlined} from '@ant-design/icons';
import axios from 'axios';
import logo from '../../assets/images/logo.png';
import './SignIn.css';

const SignIn = () => {
  const onSubmit = values => {
    axios.post('/login', values).
        then(response => {
          if (response.status === 200) {
            const data = response.data;
            if (typeof data === 'object') {
              localStorage.setItem('userData', JSON.stringify(data));
              window.location.reload();
            } else if (data === 'User Not found') {
              message.warning('User not fount');
            } else if (data === 'Invalid value for Password') {
              message.warning('Password is incorrect');
            }
          } else message.error('Something went wrong. Please try again.');
        }).
        catch(() => message.error('Something went wrong. Please try again.'));
  };

  return (
      <Row>
        <Col span={24} className="text-center">
          <img className="sign-in-logo" src={logo} alt="logo"/>
          <div className="sign-in-section text-left">
            <Form
                name="login-form"
                className="login-form"
                onFinish={onSubmit}
            >
              <Form.Item
                  name="email"
                  rules={[
                    {
                      required: true,
                      message: 'Please input your email id!',
                    },
                  ]}
              >
                <Input
                    prefix={<UserOutlined className="site-form-item-icon"/>}
                    type="email"
                    placeholder="Email"
                    size="large"
                />
              </Form.Item>
              <Form.Item
                  name="password"
                  rules={[
                    {
                      required: true,
                      message: 'Please input your Password!',
                    },
                  ]}
              >
                <Input.Password
                    prefix={<LockOutlined className="site-form-item-icon"/>}
                    type="password"
                    placeholder="Password"
                    size="large"
                />
              </Form.Item>
              <Form.Item>
                <Button
                    type="primary"
                    size="large"
                    htmlType="submit"
                    className="login-form-button"
                >
                  Log in
                </Button>
              </Form.Item>
              <p className="login-form-bottom-text">Not a member yet? <Link
                  to="/sign-up/">register now!</Link></p>
            </Form>
          </div>
        </Col>
      </Row>
  );
};

export default SignIn;
