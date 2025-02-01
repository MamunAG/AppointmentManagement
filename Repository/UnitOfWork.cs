using AppointmentManagement.lib;
using AppointmentManagement.Repository;
using ClickErp.Api.IRepository;

namespace ClickErp.Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
           => _dbContext.SaveChanges();
        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();
        public void Rollback()
            => _dbContext.Dispose();
        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();

        //==================================================================================================================================

        private IDoctorRepository _Doctor;
        public IDoctorRepository Doctor
        {
            get { return _Doctor = _Doctor ?? new DoctorRepository(_dbContext); }
        }

        private IAppointmentRepository _Appointment;
        public IAppointmentRepository Appointment
        {
            get { return _Appointment = _Appointment ?? new AppointmentRepository(_dbContext); }
        }




    }
}
