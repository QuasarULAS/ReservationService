using System;
using Infrastructure.Repositories.UserRepo;
using Infrastructure.Repositories.AuthRepo;
using Infrastructure.Repositories.BookingRepo;
using Infrastructure.Repositories.PlaceRepo;

using System.Data;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable, Infrastructure.Repositories.IUnitOfWork
    {
        public IJWTManagerRepository Auth { get; }
        public IUserRepository User { get; }
        public IBookingRepository Booking { get; }
        public IPlaceRepository Place { get; }

        public IDbConnection DbConnection { get; }
        public DbSession _session { get; }
        public Task SaveAsync();
    }
}