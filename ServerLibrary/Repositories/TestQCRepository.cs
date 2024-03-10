using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class TestQCRepository : GenericRepository<TestQC>, ITestQCRepository
    {
        public TestQCRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
