using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class SupplyRepository : GenericRepository<Supply>, ISupplyRepository
    {
        public SupplyRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
