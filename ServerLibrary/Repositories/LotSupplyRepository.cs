using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class LotSupplyRepository : GenericRepository<LotSupply>, ILotSupplyRepository
    {
        public LotSupplyRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
