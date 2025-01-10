using Microsoft.Data.SqlClient;

namespace ExamApp.Data.Concrete.ADO.NET.Helpers
{
    public static class StudentsTable
    {
        public static void Create(string connectionString)
        {
            string createStudentsTableQuery = GetQuery();

            string seedDataQuery = GetSeedData();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create the Students table if it doesn't exist
                    SqlCommand createCommand = new SqlCommand(createStudentsTableQuery, connection);
                    createCommand.ExecuteNonQuery();

                    // Insert the seed data into the Students table
                    SqlCommand insertCommand = new SqlCommand(seedDataQuery, connection);
                    insertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating Students table or inserting seed data: {ex.Message}", ex);
                }
            }
        }
        private static string GetQuery()
        {
            return @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Students' AND xtype = 'U')
                        BEGIN
                            CREATE TABLE Students (
                                Id UNIQUEIDENTIFIER PRIMARY KEY,    
                                Number INT NOT NULL UNIQUE,        
                                FirstName VARCHAR(30) NOT NULL,    
                                LastName VARCHAR(30) NOT NULL,      
                                Class INT CHECK (Class BETWEEN 1 AND 12) NOT NULL  
                            );
                        END
                    ";
        }

        private static string GetSeedData()
        {
            return @"
                        INSERT INTO Students (Id, Number, FirstName, LastName, Class) VALUES
                        (NEWID(), 1001, 'Rashad', 'Ahmedov', 10),
                        (NEWID(), 1002, 'Leyla', 'Huseynova', 12),
                        (NEWID(), 1003, 'Tural', 'Mammadov', 11),
                        (NEWID(), 1004, 'Nigar', 'Rahimova', 10),
                        (NEWID(), 1005, 'Cavid', 'Asadov', 9),
                        (NEWID(), 1006, 'Zeyneb', 'Guliyeva', 11),
                        (NEWID(), 1007, 'Kamran', 'Abdullayev', 10),
                        (NEWID(), 1008, 'Aylin', 'Rustamova', 12),
                        (NEWID(), 1009, 'Elvin', 'Ismayilov', 9),
                        (NEWID(), 1010, 'Nermin', 'Meherremova', 12),
                        (NEWID(), 1011, 'Rauf', 'Suleymanov', 10),
                        (NEWID(), 1012, 'Gunay', 'Babayev', 11);
                    ";
        } 
    }
}
