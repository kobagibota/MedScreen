using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class StandardRepository : GenericRepository<Standard>, IStandardRepository
    {
        public StandardRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
