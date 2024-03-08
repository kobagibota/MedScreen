using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private member

        private readonly AppDbContext _dbContext;
        private ILaboratoryRepository _laboratoryRepository;
        private IQCActionRepository _qcActionRepository;

        #endregion

        #region Contrastor

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        public ILaboratoryRepository LaboratoryRepository
        {
            get { return _laboratoryRepository ??= new LaboratoryRepository(_dbContext); }
        }

        public IQCActionRepository  QCActionRepository 
        {
            get { return _qcActionRepository ??= new QCActionRepository(_dbContext); }
                
        }

        #endregion

        #region Methods

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        public void Rollback()
        {
            _dbContext.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.DisposeAsync();
        }

        #endregion
    }
}
