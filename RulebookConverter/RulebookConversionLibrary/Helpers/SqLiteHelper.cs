using SQLite;

namespace RulebookConversionLibrary.Helpers
{
    public static class SqLiteHelper
    {
        private const int Version = 3;

        public static SQLiteConnection GetDatabaseInstance(string dbName)
        {
            return new SQLiteConnection(
                $"{dbName}.db{Version}",
                SQLiteOpenFlags.Create);
        }

        public static SQLiteConnection CreateConnection(string dbName)
        {
            return new SQLiteConnection(dbName);
        }
    }
}