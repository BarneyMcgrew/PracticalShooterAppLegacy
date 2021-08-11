using Foundation;
using PracticalShooterApp.iOS.Repositories;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseRepository))]
namespace PracticalShooterApp.iOS.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        public SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "rulesData.db";

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string path = Path.Combine(libFolder, sqliteFilename);

            // This is where we copy in the pre-created database
            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("rulesData", "db");
                File.Copy(existingDb, path);
            }

            var connection = new SQLiteConnection(path);

            // Return the database connection 
            return connection;
        }
    }
}