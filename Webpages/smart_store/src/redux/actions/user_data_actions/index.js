export const storeUserData = data => {
  const {id, firstName, lastName, email} = data;
  return {
    type: 'update_user_data',
    payload: {
      id,
      firstName,
      lastName,
      email,
    },
  };
};
