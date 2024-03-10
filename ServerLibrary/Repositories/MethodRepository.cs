using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class MethodRepository : GenericRepository<Method>, IMethodRepository
    {
        public MethodRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
