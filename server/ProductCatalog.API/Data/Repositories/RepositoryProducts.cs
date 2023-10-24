using ProductCatalog.API.Data.Entities.Products;

namespace ProductCatalog.API.Data.Repositories;

public class RepositoryProducts : BaseRepository<Product>, IRepositoryProducts
{
    public RepositoryProducts(ApplicationContext context)
        : base(context)
    {
    }
}