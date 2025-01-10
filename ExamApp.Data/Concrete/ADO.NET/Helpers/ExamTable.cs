using Microsoft.Data.SqlClient;

namespace ExamApp.Data.Concrete.ADO.NET.Helpers
{
    public static class ExamTable
    {
        public static void Create(string connectionString)
        {
            string createExamsTableQuery = GetQuery();

            string seedExamsTableQuery = GetSeedData();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create the Exams table if it does not exist
                    SqlCommand createTableCommand = new SqlCommand(createExamsTableQuery, connection);
                    createTableCommand.ExecuteNonQuery();

                    // Seed the Exams table with data
                    SqlCommand seedDataCommand = new SqlCommand(seedExamsTableQuery, connection);
                    seedDataCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating and seeding Exams table: {ex.Message}", ex);
                }
            }
        }
        private static string GetSeedData()
        {
            return @"
                        INSERT INTO Exams (Id, LessonCode, StudentNumber, ExamDate, Grade) VALUES
                        (NEWID(), 'RIY', 1001, '2025-01-01', 9),
                        (NEWID(), 'BIO', 1002, '2025-01-02', 8),
                        (NEWID(), 'KIM', 1003, '2025-01-03', 7),
                        (NEWID(), 'ING', 1004, '2025-01-04', 5),
                        (NEWID(), 'TAR', 1005, '2025-01-05', 6);
                    ";
        }

        private static string GetQuery()
        {
            return @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Exams' AND xtype = 'U')
                        BEGIN
                            CREATE TABLE Exams (
                                Id UNIQUEIDENTIFIER PRIMARY KEY,  
                                LessonCode CHAR(3) NOT NULL,     
                                StudentNumber INT CHECK (StudentNumber BETWEEN 0 AND 99999) NOT NULL,  
                                ExamDate DATE NOT NULL,  
                                Grade INT CHECK (Grade BETWEEN 0 AND 9) NOT NULL,  
                                CONSTRAINT UC_LessonCode_StudentNumber UNIQUE (LessonCode, StudentNumber),
                                CONSTRAINT FK_Lesson FOREIGN KEY (LessonCode) REFERENCES Lessons(LessonCode),
                                CONSTRAINT FK_Student FOREIGN KEY (StudentNumber) REFERENCES Students(Number)
                            );
                        END
                    ";
        }
    }
}
