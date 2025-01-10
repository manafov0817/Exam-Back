using Microsoft.Data.SqlClient;

namespace ExamApp.Data.Concrete.ADO.NET.Helpers
{
    public static class LessonsTable
    {
        public static void Create(string connectionString)
        {
            string createLessonsTableQuery = GetQuery();

            string seedDataQuery = GetSeedData();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create the Lessons table if it doesn't exist
                    SqlCommand createCommand = new SqlCommand(createLessonsTableQuery, connection);
                    createCommand.ExecuteNonQuery();

                    // Insert the seed data into the Lessons table
                    SqlCommand insertCommand = new SqlCommand(seedDataQuery, connection);
                    insertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating Lessons table or inserting seed data: {ex.Message}", ex);
                }
            }
        }

        private static string GetQuery()
        {
            return @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Lessons' AND xtype = 'U')
                BEGIN
                    CREATE TABLE Lessons (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,  
                        LessonCode CHAR(3) NOT NULL,     
                        LessonName VARCHAR(30) NOT NULL,  
                        Class INT CHECK (Class BETWEEN 1 AND 99),  
                        TeacherFirstName VARCHAR(20) NOT NULL,  
                        TeacherLastName VARCHAR(20) NOT NULL,   
                        CONSTRAINT UC_LessonCode UNIQUE (LessonCode)  
                    );
                END";
        }

        private static string GetSeedData()
        {
            return @"
                        INSERT INTO Lessons (Id, LessonCode, LessonName, Class, TeacherFirstName, TeacherLastName) VALUES
                        (NEWID(), 'RIY', 'Riyaziyyat', 10, 'Rashad', 'Ahmedov'),
                        (NEWID(), 'BIO', 'Biologiya', 5, 'Leyla', 'Huseynova'),
                        (NEWID(), 'KIM', 'Kimya', 11, 'Tural', 'Mammadov'),
                        (NEWID(), 'ING', 'Ingilis dili', 10, 'Nigar', 'Rahimova'),
                        (NEWID(), 'TAR', 'Tarix', 9, 'Cavid', 'Asadov'),
                        (NEWID(), 'FIZ', 'Fizika', 11, 'Zeyneb', 'Guliyeva'),
                        (NEWID(), 'BED', 'Beden terbiyesi', 10, 'Kamran', 'Abdullayev'),
                        (NEWID(), 'MUS', 'Musiqi', 12, 'Aylin', 'Rustamova'),
                        (NEWID(), 'INC', 'Incəsənət', 9, 'Elvin', 'Ismayilov'),
                        (NEWID(), 'COG', 'Cografiya', 12, 'Nermin', 'Meherremova'),
                        (NEWID(), 'INF', 'Informatika', 10, 'Rauf', 'Suleymanov'),
                        (NEWID(), 'EDB', 'Edebiyyat', 11, 'Gunay', 'Babayev');
                    ";
        }
    }
}
