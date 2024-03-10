using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class UseWithRepository : GenericRepository<UseWith>, IUseWithRepository
    {
        public UseWithRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
