using AppointmentManagement.Repository;

namespace ClickErp.Api.IRepository
{
    public interface IUnitOfWork
    {
        void Commit();//
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
        //=======================================================================================

        IDoctorRepository Doctor { get; }
        IAppointmentRepository Appointment { get; }





    }
}
