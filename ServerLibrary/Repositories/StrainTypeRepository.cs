using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class StrainTypeRepository : GenericRepository<StrainType>, IStrainTypeRepository
    {
        public StrainTypeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
