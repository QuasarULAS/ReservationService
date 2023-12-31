﻿using System.Data;
using Infrastructure.Repositories.AuthRepo;
using Infrastructure.Repositories.BookingRepo;
using Infrastructure.Repositories.PlaceRepo;

namespace Infrastructure;

public class UnitOfWork : Infrastructure.Repositories.UnitOfWork, Infrastructure.IUnitOfWork
{
    public bool IsDisposed { get; set; }
    public IDbConnection DbConnection { get; }
    public DbSession _session { get; }
    public IAuthRepository Auth { get; }
    public IBookingRepository Booking { get; }
    public IPlaceRepository Place { get; }

    public UnitOfWork(

       IAuthRepository jwtRepository,
       IBookingRepository bookingRepository,
       IPlaceRepository placeRepository,
       IDbConnection dbConnection,
       DbSession dbSession
        ) : base(dbSession)
    {
        DbConnection = dbConnection;
        Auth = jwtRepository;
        Booking = bookingRepository;
        Place = placeRepository;
        _session = dbSession;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        if (disposing) { }
        IsDisposed = true;
    }

    protected void NewRollBack()
    {
        _session.Transaction.Rollback();
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        DbConnection.Dispose();
    }
}


