using System;
using Infrastructure.Repositories.BookingRepo.Model;

namespace Infrastructure.Repositories.BookingRepo
{
    public interface IBookingRepository
    {
        Task<List<BookLogVM>> BookLog();
        Task<BookLogRecordsVM> GetBookLogRecords(MakeBookLogWithoutUserIdDto bookLog);
        Task<bool> InsertBookLog(MakeBookLogWithoutUserIdDto bookLog,
        string bookingPersonId);
    }
}

