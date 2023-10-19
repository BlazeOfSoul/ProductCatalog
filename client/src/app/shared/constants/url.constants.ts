export const actionRoutes = {
  authSignup: 'signup',
  authToken: '/connect/token',

  userInfo: '',
  userChangeInfo: '',

  authCheckUniqueUserName: 'check-unique-user-name',
  authCheckUniqueEmail: 'check-unique-email',
};


export const controllerRoutes = {
  auth: '/api/auth/',
  user: '/api/user/',
};

export const authTokenRequestNames = {
  grantType: 'grant_type',
  clientId: 'client_id',
  username: 'username',
  password: 'password',
  scope: 'scope',
};

export const authTokenRequestValues = {
  password: 'password',
  scope: 'IdentityServerApi openid',
};
