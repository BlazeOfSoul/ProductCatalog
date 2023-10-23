namespace ProductCatalog.API.Controllers.Routes
{
    public static class ProductRoutes
    {
        public const string GetAllProducts = "api/product/all";
        public const string GetAllByCategoryName = "api/product/all-category-name";
        public const string AddProduct = "api/product/add";
        public const string UpdateProduct = "api/product/update";
        public const string UpdateProductUser = "api/product/update-user";
        public const string DeleteProduct = "api/product/delete/{productId:guid}";
    }
}
