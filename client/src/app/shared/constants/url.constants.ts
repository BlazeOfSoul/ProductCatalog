export const actionRoutes = {
  authSignup: 'signup',
  authToken: '/connect/token',

  userInfo: '',
  userChangeInfo: '',
  getAllUsers: 'all',
  deleteUser: 'delete',

  authCheckUniqueUserName: 'check-unique-user-name',
  authCheckUniqueEmail: 'check-unique-email',

  addProduct: 'add',
  getProductsByName: 'all-category-name',
  getProducts: 'all',
  updateProducts: 'update',
  updateProductsUser: 'update-user',
  deleteProduct: 'delete',

  addCategory: 'add',
  getCategories: 'all',
  updateCategories: 'update',
  deleteCategory: 'delete',
};


export const controllerRoutes = {
  auth: '/api/auth/',
  user: '/api/user/',
  product: '/api/product/',
  category: '/api/category/'
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
  scope: 'IdentityServerApi openid roles',
};
