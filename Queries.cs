using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FamilyTree_DB_Migration_Aattempt
{
    class Queries
    {
        // INSERT Queries
        public static string insertQuery_Користувач = $"INSERT INTO \"Користувач\" (\"Логін\", \"Пароль\") VALUES ('{GeneratingRandomValuesForDB.GetRandomUserLogin()}', '{GeneratingRandomValuesForDB.GetRandomUserPassword()}');";
        public static string insertQuery_Дерево = $"INSERT INTO Дерево (\"Назва\") VALUES ('{GeneratingRandomValuesForDB.GetRandomFamilyTreeName()}');";
        public static string insertQuery_Медіа = $"INSERT INTO Медіа (\"Тип_медіа\", \"Шлях_до_файлу\", \"Позначені_особи\", \"Опис\", \"Дата\", \"Місце\", \"Головне_фото\") VALUES ('{GeneratingRandomValuesForDB.GetRandomMediaType()}', '{GeneratingRandomValuesForDB.GetRandomPath()}', {GeneratingRandomValuesForDB.GetRandomNumberOfMarkedPeople()}, '{GeneratingRandomValuesForDB.GetRandomDescription()}', '{GeneratingRandomValuesForDB.GetRandomDate()}', '{GeneratingRandomValuesForDB.GetRandomPlace()}', {GeneratingRandomValuesForDB.MainPhoto()});";
        public static string insertQuery_Особа = $"INSERT INTO Особа (\"Головна_особа\", \"Стать\", \"Прізвище\", \"Дівоче_прізвище\", \"Імя\", \"Інші_варіанти_імені\", \"Дата_народження\", \"Дата_смерті\") VALUES ({GeneratingRandomValuesForDB.isMainPerson()}, '{GeneratingRandomValuesForDB.GetRandomGender()}', '{GeneratingRandomValuesForDB.lastName()}', '{GeneratingRandomValuesForDB.maidenName()}', '{GeneratingRandomValuesForDB.firstName()}', '{GeneratingRandomValuesForDB.GetRandomNameVariants()}', '{GeneratingRandomValuesForDB.GenerateRandomDeathOrBirthDate()}', '{GeneratingRandomValuesForDB.GenerateRandomDeathOrBirthDate()}');";
        public static string insertQuery_Подія = $"INSERT INTO Подія (\"Тип_події\", \"Дата_події\", \"Місце_події\", \"Особа_id\", \"Опис\", \"Вік\") VALUES ('{GeneratingRandomValuesForDB.GenerateRandomEventType()}', '{GeneratingRandomValuesForDB.GetRandomDate()}', '{GeneratingRandomValuesForDB.GetRandomPlace()}', '{GeneratingRandomValuesForDB.GenerateRandomPersonId()}', '{GeneratingRandomValuesForDB.GetRandomDescription()}', '{GeneratingRandomValuesForDB.GenerateRandomAge()}');";
        public static string insertQuery_Дерево_Особа = $"INSERT INTO Дерево_Особа (\"id_дерева\", \"id_особи\") VALUES ('{new Random().Next(18)}', '{new Random().Next(152, 179)}');";
        public static string insertQuery_Звязок = $"INSERT INTO Звязок (\"id_особи1\", \"id_особи2\", \"Тип_звязку\") VALUES ('{new Random().Next(152, 179)}', '{new Random().Next(152, 179)}','{GeneratingRandomValuesForDB.GetRandomConnectionType()}');";
        public static string insertQuery_Медіа_Особа = $"INSERT INTO Медіа_Особа (\"id_особи\", \"id_медіа\") VALUES ('{new Random().Next(152, 179)}', '{new Random().Next(15)}');";
        public static string insertQuery_Медіа_Подія = $"INSERT INTO Медіа_Подія (\"id_події\", \"id_медіа\") VALUES ('{new Random().Next(31)}', '{new Random().Next(15)}');";
        public static string insertQuery_Користувач_Дерево = $"INSERT INTO Користувач_Дерево (\"логін_користувача\", \"id_дерева\", \"Тип_доступу\") VALUES ('{GeneratingRandomValuesForDB.GetRandomUserLoginFromDB()}', '{new Random().Next(18)}','{GeneratingRandomValuesForDB.GetRandomConnectionType()}');";
        public static string insertQuery_Спеціальний_запис = $"INSERT INTO Спеціальний_запис (\"Тип_запису\", \"Номер_будинку\", \"Священик\", \"Запис\", \"Подія_id\") VALUES ('{GeneratingRandomValuesForDB.GetRandomRecordType()}', '{new Random().Next(156)}', '{GeneratingRandomValuesForDB.GetRandomVicar()}', '{GeneratingRandomValuesForDB.GetRandomDescription()}', {new Random().Next(31)});";
        
        // SELECT Queries
        public static string selectQuery_Дерево = "SELECT * FROM \"Дерево\"";   
        public static string selectQuery_Користувач = "SELECT * FROM \"Користувач\"";
        public static string selectQuery_Медіа = "SELECT * FROM \"Медіа\"";
    }
}
