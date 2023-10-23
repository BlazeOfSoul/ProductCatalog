namespace ProductCatalog.API.Controllers.Routes
{
    namespace ProductCatalog.API.Routes
    {
        public static class CategoryRoutes
        {
            public const string GetAllCategories = "api/category/all";
            public const string AddCategory = "api/category/add";
            public const string UpdateCategory = "api/category/update";
            public const string DeleteCategory = "api/category/delete/{categoryId:guid}";
        }
    }
}
