using SQLite;

namespace PracticalShooterApp.Clients.Interfaces
{
    public interface ISqlRulesClient
    {
        SQLiteAsyncConnection GetConnection();
    }
}