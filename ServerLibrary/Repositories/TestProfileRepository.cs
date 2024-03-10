using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class TestProfileRepository : GenericRepository<TestProfile>, ITestProfileRepository
    {
        public TestProfileRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
