using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class QCProfileDetailRepository : GenericRepository<QCProfileDetail>, IQCProfileDetailRepository
    {
        public QCProfileDetailRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
