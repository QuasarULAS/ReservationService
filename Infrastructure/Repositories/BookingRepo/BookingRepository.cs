using System.Data;
using Dapper;
using Dapper.FastCrud;
using Infrastructure.Repositories.BookingRepo.Model;

namespace Infrastructure.Repositories.BookingRepo;

public class BookingRepository
{
    private static DbSession _session;

    public BookingRepository(DbSession session)
    {
        _session = session;
    }

    public async Task<List<BookLogVM>> BookLog()
    {
        List<BookLogVM> list;
        list = (await _session.Connection.QueryAsync<BookLogVM>("exec MYSP_AllReservationDetails")).ToList();
        return list;
    }

    public async Task<BookLogRecordsVM> GetBookLogRecords(MakeBookLogWithoutUserIdDto bookLog)
    {
        var bkr = new BookLogRecordsVM
        {
            ReservationDate = bookLog.ReservationDate,
            BookingPlaceId = bookLog.BookingPlaceId
        };

        BookLogRecordsVM? BooklogRecords;

        BooklogRecords = (await _session.Connection.QueryAsync<BookLogRecordsVM>("MYSP_BookLogRecords", bkr, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        return BooklogRecords;
    }

    public async Task<bool> InsertBookLog(MakeBookLogWithoutUserIdDto bookLog,
        string bookingPersonId)
    {

        var bookLogReq = new MakeBookLogWithUserIdDto
        {
            ReservationDate = bookLog.ReservationDate,
            BookingPersonId = bookingPersonId,
            BookingPlaceId = bookLog.BookingPlaceId,
            Price = bookLog.Price
        };

        await _session.Connection.InsertAsync(bookLogReq);
        return true;

    }
}