using System.Data;

namespace Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    bool CheckHavingActiveTransaction();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly DbSession _session;
    private int counter = 0;

    public UnitOfWork(DbSession session)
    {
        _session = session;
    }

    public void BeginTransaction()
    {
        if (counter == 0)
        {
            if (_session.Connection.State != ConnectionState.Open)
            {
                _session.Connection.Open();
            }
            _session.Transaction = _session.Connection.BeginTransaction();
        }
        counter += 1;
    }

    public void Commit()
    {
        if (counter == 1)
        {
            _session.Transaction.Commit();
            Dispose();
        }
        counter -= 1;
    }

    public void Rollback()
    {
        counter = 0;
        _session.Transaction.Rollback();
        Dispose();
    }

    public void Dispose() => _session.Transaction?.Dispose();

    public bool CheckHavingActiveTransaction() => _session.Transaction != null;
}
