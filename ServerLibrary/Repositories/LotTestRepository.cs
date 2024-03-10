using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class LotTestRepository : GenericRepository<LotTest>, ILotTestRepository
    {
        public LotTestRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
