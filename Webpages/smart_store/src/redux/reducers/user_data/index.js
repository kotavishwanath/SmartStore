const INITIAL_STATE = {
  id: '',
  firstName: '',
  lastName: '',
  email: '',
  phoneNumber: '',
};

export default (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case 'user_name_change':
      return {
        ...state,
        userName: action.payload,
      };
    case 'user_email_change':
      return {
        ...state,
        email: action.payload,
      };
    case 'update_user_data':
      return {
        ...state,
        id: action.payload.id,
        firstName: action.payload.firstName,
        lastName: action.payload.lastName,
        email: action.payload.email,
        // phoneNumber: action.payload.phoneNumber,
      };
    default:
      return state;
  }
}
