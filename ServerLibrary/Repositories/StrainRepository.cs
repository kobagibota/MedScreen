using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class StrainRepository : GenericRepository<Strain>, IStrainRepository
    {
        public StrainRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
