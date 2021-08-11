using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterApp.Shared.Interfaces.Repositories
{
    public interface IDatabaseRepository
    {
        SQLiteConnection CreateConnection();
    }
}
