using System.Data;
using System.Data.SqlClient;
using Dapper;
using Infrastructure.Repositories.BookingRepo.Model;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.BookingRepo;

public class BookingRepository
{
    private static string _connectionString;


    public BookingRepository(IConfiguration iconfiguration)
    {
        _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
    }

    public static List<BookLogViewModel> BookLog()
    {
        List<BookLogViewModel> list;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            list = db.Query<BookLogViewModel>("exec MYSP_AllReservationDetails").ToList();
        }
        return list;
    }

    public static int InsertBookLog(MakeBookLogWithoutUserIdDto bookLog,
        string bookingPersonId)
    {
        try
        {
            var bookLogReq = new MakeBookLogWithUserIdDto
            {
                ReservationDate = bookLog.ReservationDate,
                BookingPersonId = bookingPersonId,
                BookingPlaceId = bookLog.BookingPlaceId,
                Price = bookLog.Price
            };

            var bkr = new BookLogRecordsDto
            {
                ReservationDate = bookLog.ReservationDate,
                BookingPlaceId = bookLog.BookingPlaceId
            };

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var BooklogRecords =
                    db.Query<BookLogRecordsDto>("MYSP_BookLogRecords", bkr, commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();

                if (BooklogRecords != null) return 2;

                var query =
                    "insert into BookLog(ReservationDate,BookingPersonId,BookingPlaceId,Price) values(@ReservationDate,@BookingPersonId,@BookingPlaceId,@Price)";
                db.Execute(query, bookLogReq);
            }
            return 0;
        }
        catch (Exception)
        {
            return 1;
        }
    }
}