using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.DependencyInterfaces;
using PracticalShooterApp.Enums;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PracticalShooterApp.Clients
{
    public class SqlRulesClient : ISqlRulesClient
    {
        private static string RULESDBNAME = "rulesData.db";
        private SQLiteAsyncConnection _dbRules;
        
        public SqlRulesClient()
        {
            InitialiseDatabase();
        }

        public void InitialiseDatabase()
        {
            string databasePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), RULESDBNAME);

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            
            Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("PracticalShooterApp.Databases." + RULESDBNAME);

            if (!File.Exists(databasePath))
            {
                FileStream fileStreamToWrite = File.Create(databasePath);
                embeddedDatabaseStream.Seek(0, SeekOrigin.Begin);
                embeddedDatabaseStream.CopyTo(fileStreamToWrite);
                fileStreamToWrite.Close();
            }

            _dbRules = new SQLiteAsyncConnection(databasePath);
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _dbRules;
        }
    }
}