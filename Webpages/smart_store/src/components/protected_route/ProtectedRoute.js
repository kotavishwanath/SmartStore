import React from 'react';
import {Redirect, Route} from 'react-router-dom';

const ProtectedRoute = ({children, isPublic, ...rest}) => {
  return (
      <Route
          {...rest}
          render={({location}) =>
              isPublic ? (
                  children
              ) : (
                  <Redirect
                      to={{
                        pathname: '/sign-in/',
                        state: {from: location},
                      }}
                  />
              )
          }
      />
  );
};

export default ProtectedRoute;
