using ProductCatalog.API.Data.Entities.Users;

namespace ProductCatalog.API.Data.Repositories;

public class RepositoryUsers : BaseRepository<User>, IRepositioryUsers
{
    public RepositoryUsers(ApplicationContext context)
        : base(context)
    {
    }
}