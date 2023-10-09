using System;
using Npgsql;

namespace FamilyTree_DB_Migration_Aattempt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connectionString = "Host=localhost;Port=5432;Database=FamilyTreeAttempt;Username=postgres;Password=123321123;";
            string testing = GeneratingRandomValuesForDB.GetRandomFamilyTreeName();
            Console.WriteLine(testing);
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Відкриваємо підключення до бази даних
                    string insertQuery = $"INSERT INTO Дерево (\"Назва\") VALUES ('{GeneratingRandomValuesForDB.GetRandomFamilyTreeName()}');"; 
                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    string selectQuery = "SELECT * FROM Дерево";
                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetString(1)}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Закриваємо підключення до бази даних
                }

            }
        }
    }
}
