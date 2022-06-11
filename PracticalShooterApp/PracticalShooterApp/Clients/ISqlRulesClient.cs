using SQLite;

namespace PracticalShooterApp.Clients
{
    public interface ISqlRulesClient
    {
        SQLiteAsyncConnection GetConnection();
    }
}