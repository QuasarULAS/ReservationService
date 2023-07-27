using System.Data;
using Infrastructure.Repositories.AuthRepo;
using Infrastructure.Repositories.BookingRepo;
using Infrastructure.Repositories.PlaceRepo;

namespace Infrastructure;

public interface IUnitOfWork : IDisposable, Infrastructure.Repositories.IUnitOfWork
{
    public IAuthRepository Auth { get; }
    public IBookingRepository Booking { get; }
    public IPlaceRepository Place { get; }

    public IDbConnection DbConnection { get; }
    public DbSession _session { get; }
    public Task SaveAsync();
}
