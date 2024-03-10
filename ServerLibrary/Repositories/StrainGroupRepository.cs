using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class StrainGroupRepository : GenericRepository<StrainGroup>, IStrainGroupRepository
    {
        public StrainGroupRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
