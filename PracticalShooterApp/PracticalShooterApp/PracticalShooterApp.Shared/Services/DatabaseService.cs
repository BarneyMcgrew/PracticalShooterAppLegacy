using PracticalShooterApp.Shared.DataModels;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PracticalShooterApp.Shared.Services
{
    public class DatabaseService
    {
        SQLiteConnection _dbConnection;

        public DatabaseService()
        {
            _dbConnection = DependencyService.Get<IDatabaseRepository>().CreateConnection();
        }

        public void TestDatabaseConnection()
        {
            var query = _dbConnection.Query<Rulebook>("SELECT * FROM Rulebook");
        }
    }
}
