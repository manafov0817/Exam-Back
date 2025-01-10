using Microsoft.Data.SqlClient;


namespace ExamApp.Data.Concrete.ADO.NET.Helpers
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InitializeDatabase()
        {
            try
            {
                // Step 1: Ensure the database exists
                EnsureDatabaseExists();

                // Step 2: Create tables if they do not exist
                CreateTables();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error initializing database: {ex.Message}");
            }
        }

        private void EnsureDatabaseExists()
        {
            string connectionStringToMaster = _connectionString.Replace("ExamDB", "master");

            using (var connection = new SqlConnection(connectionStringToMaster))
            {
                try
                {
                    connection.Open();

                    // Query to check if the database exists
                    string checkDbQuery = @"
                        IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ExamDB')
                        BEGIN
                            CREATE DATABASE ExamDB;
                        END";

                    SqlCommand command = new SqlCommand(checkDbQuery, connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking/creating database: {ex.Message}", ex);
                }
            }
        }

        private void CreateTables()
        {
            // Do not change the order of table creation
            LessonsTable.Create(_connectionString);
            StudentsTable.Create(_connectionString);
            ExamTable.Create(_connectionString);
        }
    }
}
