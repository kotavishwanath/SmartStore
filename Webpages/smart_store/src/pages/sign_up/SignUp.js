import React from 'react';
import {Link, withRouter} from 'react-router-dom';
import {Button, Col, Form, Input, message, Row, Select} from 'antd';
import {LockOutlined, UserOutlined} from '@ant-design/icons';
import axios from 'axios';
import logo from '../../assets/images/logo.png';
import './SignUp.css';

const {Option} = Select;

const prefixSelector = (
    <Form.Item name="prefix" noStyle>
      <Select style={{width: 70}}>
        <Option value="86">+86</Option>
        <Option value="87">+87</Option>
        <Option value="91">+91</Option>
      </Select>
    </Form.Item>
);

const SignUp = props => {
  const onSubmit = values => {
    const {first_name, last_name, email, prefix, phone_number, password} = values;

    const data = {
      firstName: first_name,
      lastName: last_name,
      gender: 0,
      email,
      password,
      phone: prefix + '-' + phone_number,
    };

    axios.post('/login/saveuser', data).
        then(response => {
          if (response.status === 200) {
            if (response.data === 'User Registration successful') {
              message.success('Successfully registered');
              props.history.push('/sign-in/');
            } else if (response.data === 'User already exist. Please try with different email') {
              message.warning('User with this details already found.');
            } else message.error('Something went wrong. Please try again.');
          }
        }).
        catch(() => message.error('Something went wrong. Please try again.'));
  };

  return (
      <Row>
        <Col span={24} className="text-center">
          <img className="sign-up-logo" src={logo} alt="logo"/>
          <div className="sign-up-section text-left">
            <Form
                name="sign-up-form"
                className="sign-up-form"
                initialValues={{
                  prefix: '86',
                }}
                onFinish={onSubmit}
            >
              <Form.Item
                  name="first_name"
                  rules={[
                    {
                      required: true,
                      message: 'Please input your first name!',
                    },
                  ]}
              >
                <Input
                    prefix={<UserOutlined className="site-form-item-icon"/>}
                    placeholder="First Name"
                    size="large"
                />
              </Form.Item>
              <Form.Item
                  name="last_name"
                  rules={[
                    {
                      required: true,
                      message: 'Please input your last name!',
                    },
                  ]}
              >
                <Input
                    prefix={<UserOutlined className="site-form-item-icon"/>}
                    placeholder="Last Name"
                    size="large"
                />
              </Form.Item>
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
                  name="phone_number"
                  rules={[
                    {
                      required: true,
                      message: 'Please input your phone number!',
                    },
                  ]}
              >
                <Input
                    size="large"
                    placeholder="Phone Number"
                    addonBefore={prefixSelector}
                    style={{width: '100%'}}
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
              <Form.Item
                  name="confirm_password"
                  dependencies={['password']}
                  rules={[
                    {
                      required: true,
                      message: 'Please input your Password!',
                    },
                    ({getFieldValue}) => ({
                      validator(rule, value) {
                        if (!value || getFieldValue('password') === value) {
                          return Promise.resolve();
                        }
                        return Promise.reject(
                            'The two passwords that you entered do not match!');
                      },
                    }),
                  ]}
              >
                <Input.Password
                    prefix={<LockOutlined className="site-form-item-icon"/>}
                    type="password"
                    placeholder="Confirm Password"
                    size="large"
                />
              </Form.Item>
              <Form.Item>
                <Button
                    type="primary"
                    size="large"
                    htmlType="submit"
                    className="sign-up-form-button"
                >
                  Sign Up
                </Button>
              </Form.Item>
              <p className="sign-up-form-bottom-text">Already registered? <Link
                  to="/sign-in/">Login</Link></p>
            </Form>
          </div>
        </Col>
      </Row>
  );
};

export default withRouter(SignUp);
