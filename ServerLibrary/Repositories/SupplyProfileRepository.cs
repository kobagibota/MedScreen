using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class SupplyProfileRepository : GenericRepository<SupplyProfile>, ISupplyProfileRepository
    {
        public SupplyProfileRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
