using FamilyTree_DB_Migration_Aattempt;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime;
namespace FamilyTree_DB_Migration_Aattempt
{
    class Queries
    {
<<<<<<< HEAD
        public static string connectionString = "Host=localhost;Port=5432;Database=FamilyTree;Username=postgres;Password=yhy121352;";

=======
        public static string connectionString = "Host=localhost;Port=5432;Database=FamilyTreeAttempt;Username=postgres;Password=123321123;";  
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
        public static void InsertData(string connectionString, string insertQuery, Dictionary<string, object> parameters)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        int rowsAffected = command.ExecuteNonQuery();
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
        public static void SelectData(string connectionString, string selectQuery, Action<NpgsqlDataReader> processResult)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            processResult(reader);
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
<<<<<<< HEAD
<<<<<<< HEAD
        public static void UpdateData(string connectionString, string updateQuery, Dictionary<string, object> parameters)
=======
=======
        public static void DeleteCertainData(string connectionString, string deleteQuery, Dictionary<string, object> parameters)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection))
                    {
                        // Виконати SQL-запит
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Дані були успішно видалені.");
                        }
                        else
                        {
                            Console.WriteLine("Не вдалося знайти дані для видалення.");
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
>>>>>>> b0fa83831072e8029a887d0c9cab490d0136eb69
        public static void DeleteAllData(string connectionString, string deleteQuery)
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
<<<<<<< HEAD
                    using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                        int rowsAffected = command.ExecuteNonQuery();
=======
                    using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection))
                    {
                        // Виконати SQL-запит
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Дані були успішно видалені.");
                        }
                        else
                        {
                            Console.WriteLine("Не вдалося знайти дані для видалення.");
                        }
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
<<<<<<< HEAD
                    connection.Close();
                }
            }
        }

=======
                    connection.Close(); // Закриваємо підключення до бази даних
                }
            }
        }
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
        // INSERT Queries
        public static void InsertQuery_Користувач()
        {
            string insertQuery = "INSERT INTO \"Користувач\" (\"Логін\", \"Пароль\") VALUES (@login, @password)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@login", GeneratingRandomValuesForDB.GetRandomUserLogin() },
                { "@password", GeneratingRandomValuesForDB.GetRandomUserPassword() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void insertQuery_Дерево()
        {
            string insertQuery = "INSERT INTO Дерево (\"Назва\") VALUES (@name)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@name", GeneratingRandomValuesForDB.GetRandomFamilyTreeName()},
                { "@password", GeneratingRandomValuesForDB.GetRandomUserPassword() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Медіа()
        {
            string insertQuery = "INSERT INTO Медіа (\"Тип_медіа\", \"Шлях_до_файлу\", \"Позначені_особи\", \"Опис\", \"Дата\", \"Місце\", \"Головне_фото\") " +
                                "VALUES (@mediaType, @filePath, @numberOfMarkedPeople, @description, @date, @place, @isMainPhoto)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@mediaType", GeneratingRandomValuesForDB.GetRandomMediaType() },
                { "@filePath", GeneratingRandomValuesForDB.GetRandomPath() },
                { "@numberOfMarkedPeople", GeneratingRandomValuesForDB.GetRandomNumberOfMarkedPeople() },
                { "@description", GeneratingRandomValuesForDB.GetRandomDescription() },
                { "@date", DateTime.Parse(GeneratingRandomValuesForDB.GetRandomDate()) },
                { "@place", GeneratingRandomValuesForDB.GetRandomPlace() },
                { "@isMainPhoto", GeneratingRandomValuesForDB.MainPhoto() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Особа()
        {
            string insertQuery = "INSERT INTO Особа (\"Головна_особа\", \"Стать\", \"Прізвище\", \"Дівоче_прізвище\", \"Імя\", \"Інші_варіанти_імені\", \"Дата_народження\", \"Дата_смерті\") " +
                                "VALUES (@isMainPerson, @gender, @lastName, @maidenName, @firstName, @nameVariants, @birthDate, @deathDate)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@isMainPerson", GeneratingRandomValuesForDB.isMainPerson() },
                { "@gender", GeneratingRandomValuesForDB.GetRandomGender() },
                { "@lastName", GeneratingRandomValuesForDB.lastName() },
                { "@maidenName", GeneratingRandomValuesForDB.maidenName() },
                { "@firstName", GeneratingRandomValuesForDB.firstName() },
                { "@nameVariants", GeneratingRandomValuesForDB.GetRandomNameVariants() },
                { "@birthDate", DateTime.Parse(GeneratingRandomValuesForDB.GenerateRandomDeathOrBirthDate())},
                { "@deathDate", DateTime.Parse(GeneratingRandomValuesForDB.GenerateRandomDeathOrBirthDate()) } };
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Подія()
        {
            string insertQuery = "INSERT INTO Подія (\"Тип_події\", \"Дата_події\", \"Місце_події\", \"Особа_id\", \"Опис\", \"Вік\") " +
                                "VALUES (@eventType, @eventDate, @eventPlace, @personId, @eventDescription, @eventAge)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@eventType", GeneratingRandomValuesForDB.GenerateRandomEventType() },
                { "@eventDate", DateTime.Parse(GeneratingRandomValuesForDB.GetRandomDate()) },
                { "@eventPlace", GeneratingRandomValuesForDB.GetRandomPlace() },
                { "@personId", GeneratingRandomValuesForDB.GetRandomID("Особа") },
                { "@eventDescription", GeneratingRandomValuesForDB.GetRandomDescription() },
                { "@eventAge", GeneratingRandomValuesForDB.GenerateRandomAge() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Дерево_Особа()
        {
            string insertQuery = "INSERT INTO Дерево_Особа (\"id_дерева\", \"id_особи\") " +
                                "VALUES (@treeId, @personId)";
<<<<<<< HEAD
            Dictionary<string, object> parameters = new Dictionary<string, object> {
=======
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
<<<<<<< HEAD
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
                { "@treeId", new Random().Next(1, SelectCount("Дерево") + 1) },
                { "@personId", new Random().Next(1, SelectCount("Особа") + 1) }};
=======
                { "@treeId", GeneratingRandomValuesForDB.GetRandomID("Дерево") },
                { "@personId", GeneratingRandomValuesForDB.GetRandomID("Особа") }};
>>>>>>> 7ce157b989ed3a3f4f216bce01377b116df1bb13
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Звязок()
        {
            string insertQuery = "INSERT INTO Звязок (\"id_особи1\", \"id_особи2\", \"Тип_звязку\") " +
                                "VALUES (@personId1, @personId2, @connectionType)";
<<<<<<< HEAD
            Dictionary<string, object> parameters = new Dictionary<string, object> {
=======
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
<<<<<<< HEAD
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
                { "@personId1", new Random().Next(1, SelectCount("Дерево") + 1) },
                { "@personId2", new Random().Next(1, SelectCount("Дерево") + 1) },
=======
                { "@personId1", GeneratingRandomValuesForDB.GetRandomID("Особа") },
                { "@personId2", GeneratingRandomValuesForDB.GetRandomID("Особа") },
>>>>>>> 7ce157b989ed3a3f4f216bce01377b116df1bb13
                { "@connectionType", GeneratingRandomValuesForDB.GetRandomConnectionType() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Медіа_Особа()
        {
            string insertQuery = "INSERT INTO Медіа_Особа (\"id_особи\", \"id_медіа\") " +
                                "VALUES (@personId, @mediaId)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@personId", GeneratingRandomValuesForDB.GetRandomID("Особа") },
                { "@mediaId", GeneratingRandomValuesForDB.GetRandomID("Медіа") } };
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Медіа_Подія()
        {
            string insertQuery = "INSERT INTO Медіа_Подія (\"id_події\", \"id_медіа\") " +
                                "VALUES (@eventId, @mediaId)";
<<<<<<< HEAD
            Dictionary<string, object> parameters = new Dictionary<string, object> {
=======
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
<<<<<<< HEAD
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
                { "@eventId", new Random().Next(1, SelectCount("Подія") + 1) },
                { "@mediaId", new Random().Next(1, SelectCount("Медіа") + 1) }};
=======
                { "@eventId", GeneratingRandomValuesForDB.GetRandomID("Подія") },
                { "@mediaId", GeneratingRandomValuesForDB.GetRandomID("Медіа") }};
>>>>>>> 7ce157b989ed3a3f4f216bce01377b116df1bb13
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Користувач_Дерево()
        {
            string insertQuery = "INSERT INTO Користувач_Дерево (\"логін_користувача\", \"id_дерева\", \"Тип_доступу\") " +
                                "VALUES (@userLogin, @treeId, @accessType)";
<<<<<<< HEAD
            Dictionary<string, object> parameters = new Dictionary<string, object> {
=======
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
                { "@userLogin", GeneratingRandomValuesForDB.GetRandomUserLoginFromDB() },
                { "@treeId", GeneratingRandomValuesForDB.GetRandomID("Дерево") },
                { "@accessType", GeneratingRandomValuesForDB.GetRandomAccessType() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Спеціальний_запис()
        {
            string insertQuery = "INSERT INTO Спеціальний_запис (\"Тип_запису\", \"Номер_будинку\", \"Священик\", \"Запис\", \"Подія_id\") " +
                                "VALUES (@type, @number, @priest, @record, @id)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@type",  GeneratingRandomValuesForDB.GetRandomRecordType()},
                { "@number",  new Random().Next(156)},
                { "@priest",  GeneratingRandomValuesForDB.GetRandomVicar()},
                { "@record", GeneratingRandomValuesForDB.GetRandomDescription() },
                { "@id", GeneratingRandomValuesForDB.GetRandomID("Подія") } };
            InsertData(connectionString, insertQuery, parameters);
        }
        // SELECT Queries
        public static void SelectДерево()
        {
            string selectQuery = "SELECT * FROM \"Дерево\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Дерево"
                    Console.WriteLine($"Дерево ID: {reader.GetInt32(0)}, Назва: {reader.GetString(1)}");
                }
            });
        }
        public static void SelectКористувач()
        {
            string selectQuery = "SELECT * FROM \"Користувач\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Дерево"
                    Console.WriteLine($"Логін: {reader.GetString(0)}, Пароль: {reader.GetString(1)}");
                }
            });
        }
        public static void SelectДерево_Особа()
        {
            string selectQuery = "SELECT * FROM \"Дерево_Особа\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Дерево_Особа"
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, id_дерева: {reader.GetInt32(1)}, id_особи: {reader.GetInt32(2)}");
                }
            });
        }
        public static void SelectЗвязок()
        {
            string selectQuery = "SELECT * FROM \"Звязок\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Звязок"
                    Console.WriteLine($"Звязок ID: {reader.GetInt32(0)}, id_особи1: {reader.GetInt32(1)}, id_особи2: {reader.GetInt32(2)}, тип зв'язку: {reader.GetString(3)}");
                }
            });
        }
        public static void SelectКористувач_Дерево()
        {
            string selectQuery = "SELECT * FROM \"Користувач_Дерево\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Користувач_Дерево"
                    Console.WriteLine($"Користувач_Дерево ID: {reader.GetInt32(0)}, логін_користувача: {reader.GetString(1)}, id_дерева: {reader.GetInt32(2)}, тип доступу: {reader.GetString(3)}");
                }
            });
        }
        public static void SelectМедіа()
        {
            string selectQuery = "SELECT * FROM \"Медіа\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Медіа"
                    Console.WriteLine($"Медіа ID: {reader.GetInt32(0)}, тип медіа: {reader.GetString(1)}, шлях до файлу: {reader.GetString(2)}, позначені особи: {reader.GetInt32(3).ToString()}, опис: {reader.GetString(4)}, дата: {reader.GetDateTime(5).ToString()}, місце: {reader.GetString(6)}, головне фото: {reader.GetBoolean(7)}");
                }
            });
        }
        public static void SelectМедіа_Особа()
        {
            string selectQuery = "SELECT * FROM \"Медіа_Особа\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Медіа_Особа"
                    Console.WriteLine($"Медіа_Особа ID: {reader.GetInt32(0)}, id_особи: {reader.GetInt32(1)}, id_медіа: {reader.GetInt32(2)}");
                }
            });
        }
        public static void SelectМедіа_Подія()
        {
            string selectQuery = "SELECT * FROM \"Медіа_Подія\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Медіа_Подія"
                    Console.WriteLine($"Медіа_Подія ID: {reader.GetInt32(0)}, id_події: {reader.GetInt32(1)}, id_медіа: {reader.GetInt32(2)}");
                }
            });
        }
        public static void SelectОсоба()
        {
            string selectQuery = "SELECT * FROM \"Особа\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Особа"
                    Console.WriteLine($"Особа ID: {reader.GetInt32(0)}, головна особа: {reader.GetBoolean(1)}, стать: {reader.GetString(2)}, прізвище: {reader.GetString(3)}, дівоче прізвище: {reader.GetString(4)}, ім'я: {reader.GetString(5)}, інші варіанти імені: {reader.GetString(6)}, дата народження: {reader.GetDateTime(7)}, дата смерті: {reader.GetDateTime(8)}");
                }
            });
        }
        public static void SelectСпеціальний_запис()
        {
            string selectQuery = "SELECT * FROM \"Спеціальний_запис\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Спеціальний_запис"
                    Console.WriteLine($"Спеціальний запис ID: {reader.GetInt32(0)}, тип запису: {reader.GetString(1)}, номер будинку: {reader.GetInt32(2)}, священик: {reader.GetString(3)}, запис: {reader.GetString(4)}, подія id: {reader.GetInt32(5)}");
                }
            });
        }
        public static void SelectПодія()
        {
            string selectQuery = "SELECT * FROM \"Подія\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    // Обробка результатів для таблиці "Подія"
                    Console.WriteLine($"Подія ID: {reader.GetInt32(0)}, тип події: {reader.GetString(1)}, " +
                        $"дата події: {reader.GetDateTime(2)}, місце події: {reader.GetString(3)}, " +
                        $"особа id: {reader.GetInt32(4)}, опис: {reader.GetString(5)}, вік: {reader.GetInt32(6)}");
                }
            });
        }
<<<<<<< HEAD

=======
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
        public static int SelectCount(string table_name)
        {
            int count = 0;
            string selectQuery = "SELECT COUNT (*) FROM \"" + table_name + "\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            });
            return count;
        }
<<<<<<< HEAD
        public static List<string> SelectUserNameList()
=======
        public static List<string> SelectUserNameList() 
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
        {
            List<string> list = new List<string>();
            string selectQuery = "SELECT * FROM \"Користувач\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            });
            return list;
        }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        public static void UpdateКористувач(string login, string newPassword)
        {
            string updateQuery = "UPDATE \"Користувач\" SET \"Пароль\" = @newPassword WHERE \"Логін\" = @login";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@newPassword", newPassword },
                { "@login", login }
            };
            UpdateData(connectionString, updateQuery, parameters);
        }
        public static void UpdateКористувач_Дерево(string login ,string ConnectionType)
        {
            string updateQuery = "UPDATE \"Користувач_Дерево\" SET \"Тип_доступу\" = @newPassword WHERE \"логін_користувача\" = @login";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@newPassword", ConnectionType },
                { "@login", login }
            };
            UpdateData(connectionString, updateQuery, parameters);
        }
        public static void UpdateПодія()
        {
            
            string updateQuery = "UPDATE \"Подія\" SET \"Вік\" = 0 WHERE \"Тип_події\" = 'народження' AND \"Вік\" > 0";
            UpdateData(connectionString, updateQuery, null);
        }
        public static void UpdateОсоба()
        {

            string updateQuery = "UPDATE \"Особа\" SET \"Дівоче_прізвище\" = '' WHERE \"Стать\" = 'чоловік'";
            
            UpdateData(connectionString, updateQuery, null);
        }

=======
=======
        public static List<int> SelectPersonIDList()
=======
        public static List<int> SelectIDList(string table_name)
>>>>>>> b0fa83831072e8029a887d0c9cab490d0136eb69
        {
            List<int> list = new List<int>();
            string selectQuery = "SELECT * FROM \"" + table_name + "\"";
            SelectData(connectionString, selectQuery, reader =>
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32(0));
                }
            });
            return list;
        }
<<<<<<< HEAD
>>>>>>> 7ce157b989ed3a3f4f216bce01377b116df1bb13
=======
            
>>>>>>> b0fa83831072e8029a887d0c9cab490d0136eb69
        // DELETE Queries
        // DELETE all data from a table
        public static void DELETE_ALL_FROM_Користувач_Query()
        {
            string deleteQuery = "DELETE FROM \"Користувач\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Дерево_Query()
        {
            string deleteQuery = "DELETE FROM \"Дерево\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Дерево_Особа_Query()
        {
            string deleteQuery = "DELETE FROM \"Дерево_Особа\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Звязок_Query()
        {
            string deleteQuery = "DELETE FROM \"Звязок\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Користувач_Дерево_Query()
        {
            string deleteQuery = "DELETE FROM \"Користувач_Дерево\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Медіа_Query()
        {
            string deleteQuery = "DELETE FROM \"Медіа\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Медіа_Особа_Query()
        {
            string deleteQuery = "DELETE FROM \"Медіа_Особа\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Медіа_Подія_Query()
        {
            string deleteQuery = "DELETE FROM \"Медіа_Подія\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Особа_Query()
        {
            string deleteQuery = "DELETE FROM \"Особа\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Спеціальний_запис_Query()
        {
            string deleteQuery = "DELETE FROM \"Спеціальний_запис\";";
            DeleteAllData(connectionString, deleteQuery);
        }
        public static void DELETE_ALL_FROM_Подія_Query()
        {
            string deleteQuery = "DELETE FROM \"Подія\";";
            DeleteAllData(connectionString, deleteQuery);
        }
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 53e059f241ab0f531496228560e6197c69110d88
=======
            // DELETE the certain row from a table

>>>>>>> 7ce157b989ed3a3f4f216bce01377b116df1bb13
=======
        // DELETE the certain row from a table
        public static void DeleteQuery_Медіа_Подія()
        {
            string deleteQuery = "DELETE FROM Медіа_Подія WHERE id = @eventIdМедіа_Подія";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@eventIdМедіа_Подія", GeneratingRandomValuesForDB.GetRandomID("Медіа_Подія") }
            };
            DeleteCertainData(connectionString, deleteQuery, parameters);
        }
>>>>>>> b0fa83831072e8029a887d0c9cab490d0136eb69
    }
}