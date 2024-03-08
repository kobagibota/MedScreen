using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class QCActionRepository : GenericRepository<QCAction>, IQCActionRepository
    {
        public QCActionRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
