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
        public static string connectionString = "Host=localhost;Port=5432;Database=FamilyTree;Username=postgres;Password=-------;";
       
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
                { "@personId", new Random().Next(1, SelectCount("Особа") + 1) },
                { "@eventDescription", GeneratingRandomValuesForDB.GetRandomDescription() },
                { "@eventAge", GeneratingRandomValuesForDB.GenerateRandomAge() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Дерево_Особа()
        {
            string insertQuery = "INSERT INTO Дерево_Особа (\"id_дерева\", \"id_особи\") " +
                                "VALUES (@treeId, @personId)";
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
                { "@treeId", new Random().Next(1, SelectCount("Дерево") + 1) },
                { "@personId", new Random().Next(1, SelectCount("Особа") + 1) }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Звязок()
        {
            string insertQuery = "INSERT INTO Звязок (\"id_особи1\", \"id_особи2\", \"Тип_звязку\") " +
                                "VALUES (@personId1, @personId2, @connectionType)";
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
                { "@personId1", new Random().Next(1, SelectCount("Дерево") + 1) },
                { "@personId2", new Random().Next(1, SelectCount("Дерево") + 1) },
                { "@connectionType", GeneratingRandomValuesForDB.GetRandomConnectionType() }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Медіа_Особа()
        {
            string insertQuery = "INSERT INTO Медіа_Особа (\"id_особи\", \"id_медіа\") " +
                                "VALUES (@personId, @mediaId)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@personId", new Random().Next(1, SelectCount("Дерево") + 1) },
                { "@mediaId", new Random().Next(1, SelectCount("Медіа") + 1) } };
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Медіа_Подія()
        {
            string insertQuery = "INSERT INTO Медіа_Подія (\"id_події\", \"id_медіа\") " +
                                "VALUES (@eventId, @mediaId)";
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
                { "@eventId", new Random().Next(1, SelectCount("Подія") + 1) },
                { "@mediaId", new Random().Next(1, SelectCount("Медіа") + 1) }};
            InsertData(connectionString, insertQuery, parameters);
        }
        public static void InsertQuery_Користувач_Дерево()
        {
            string insertQuery = "INSERT INTO Користувач_Дерево (\"логін_користувача\", \"id_дерева\", \"Тип_доступу\") " +
                                "VALUES (@userLogin, @treeId, @accessType)";
            Dictionary<string, object> parameters = new Dictionary<string, object> { 
                { "@userLogin", GeneratingRandomValuesForDB.GetRandomUserLoginFromDB() },
                { "@treeId", new Random().Next(1, SelectCount("Дерево") + 1) },
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
                { "@id", new Random().Next(1, SelectCount("Подія") + 1) } };
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
        public static List<string> SelectUserNameList() 
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
    }
}