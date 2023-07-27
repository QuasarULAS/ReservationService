using System;
using System.Data;

namespace Infrastructure
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IDbConnection dbConnection)
        {
            Connection = dbConnection;
            // Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
