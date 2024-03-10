using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class QCProfileRepository : GenericRepository<QCProfile>, IQCProfileRepository
    {
        public QCProfileRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
