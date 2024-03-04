using BaseLibrary.Interfaces;
using BaseLibrary.Entities;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class LaboratoryRepository : GenericRepository<Laboratory>, ILaboratoryRepository
    {
        public LaboratoryRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
