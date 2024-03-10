using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class StandardDetailRepository : GenericRepository<StandardDetail>, IStandardDetailRepository
    {
        public StandardDetailRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
