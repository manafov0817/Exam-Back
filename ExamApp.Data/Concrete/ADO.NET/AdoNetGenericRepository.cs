using ExamApp.Data.Abstract;
using ExamApp.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ExamApp.Data.Concrete.ADO.NET
{
    public class AdoNetGenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly string _connectionString;
        private readonly string _tableName = $"{typeof(T).Name}s";
        public AdoNetGenericRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateAsync(T entity)
        {
            var properties = typeof(T).GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));

            var commandText = $"INSERT INTO {_tableName} ({columnNames}) VALUES ({parameterNames})";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(commandText, connection);
                foreach (var property in properties)
                {
                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            var properties = typeof(T).GetProperties();

            // Find the primary key property (assuming it's named "Id" by convention)
            var primaryKeyProperty = properties.FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            if (primaryKeyProperty == null)
            {
                throw new InvalidOperationException("The entity does not have a primary key property named 'Id'.");
            }

            var primaryKeyValue = primaryKeyProperty.GetValue(entity);
            if (primaryKeyValue == null)
            {
                throw new ArgumentException("The entity's primary key cannot be null.");
            }

            var commandText = $"DELETE FROM {_tableName} WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", primaryKeyValue);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));

            var commandText = $"SELECT {columnNames} FROM {_tableName}";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(commandText, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = Activator.CreateInstance<T>();

                        foreach (var property in properties)
                        {
                            var value = reader[property.Name];
                            if (value != DBNull.Value)
                                property.SetValue(entity, value);
                        }

                        result.Add(entity);
                    }
                }
            }
            return result;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = default(T);
            var properties = typeof(T).GetProperties();

            // Assuming "Id" is the primary key
            var commandText = $"SELECT * FROM {_tableName} WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = Activator.CreateInstance<T>();

                        foreach (var property in properties)
                        {
                            var value = reader[property.Name];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(result, value);
                            }
                        }
                    }
                }
            }

            return result;
        }


        public async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
