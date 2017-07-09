using System.Data.SqlClient;
using Dapper;

namespace AnApiOfIceAndFire.DataFeeder
{
    public static class DatabaseFeeder
    {
        public static void CreateDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.CreateDatabase);
            }
        }

        public static void CreateTables(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.CreateBooksTable);
                connection.Execute(DatabaseScripts.CreateCharactersTable);
                connection.Execute(DatabaseScripts.CreateHousesTable);
                connection.Execute(DatabaseScripts.CreateBookCharacterLinkTable);
                connection.Execute(DatabaseScripts.CreateCharacterHouseLinkTable);
                connection.Execute(DatabaseScripts.CreateHouseCadetBranchLinkTable);
            }
        }

        public static void DeleteDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.DeleteDatabase);
            }
        }

        public static void CleanDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.TruncateAllTables);
            }
        }

    }
}