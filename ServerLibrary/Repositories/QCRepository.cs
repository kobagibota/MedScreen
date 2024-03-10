using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class QCRepository : GenericRepository<QC>, IQCRepository
    {
        public QCRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
