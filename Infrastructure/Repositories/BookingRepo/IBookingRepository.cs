using Infrastructure.Repositories.BookingRepo.Model;

namespace Infrastructure.Repositories.BookingRepo;

public interface IBookingRepository
{
    Task<List<BookLogVM>> GetAllBookLog();
    Task<BookLogRecordsVM> GetBookLogRecords(BookLogRecordsVM bkr);
    Task<bool> InsertBookLog(MakeBookLogWithoutUserIdIM bookLog,
    Guid bookingPersonId);
}


