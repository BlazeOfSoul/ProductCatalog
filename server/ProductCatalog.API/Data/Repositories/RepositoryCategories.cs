using ProductCatalog.API.Data.Entities.Categories;

namespace ProductCatalog.API.Data.Repositories;

public class RepositoryCategories : BaseRepository<Category>, IRepositoryCategoies
{
    public RepositoryCategories(ApplicationContext context)
        : base(context)
    {
    }
}