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
            string insertQuery = Queries.insertQuery_Користувач_Дерево;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Відкриваємо підключення до бази даних                                  
                    
                    Console.WriteLine(insertQuery);
                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                    }

                    //string selectQuery = Queries.selectQuery_Дерево;
                    //using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    //{
                    //    using (NpgsqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            Console.WriteLine($"{reader.GetString(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetString(3)}, {reader.GetString(4)}, {reader.GetString(5)}, {reader.GetString(6)}");
                    //        }
                    //    }
                    //}
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
