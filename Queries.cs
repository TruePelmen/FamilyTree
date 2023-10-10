using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FamilyTree_DB_Migration_Aattempt
{
    class Queries
    {
        // Domains like emuns

        // INSERT Queries
        public static string insertQuery_Користувач = $"INSERT INTO \"Користувач\" (\"Логін\", \"Пароль\") VALUES ('{GeneratingRandomValuesForDB.GetRandomUserLogin()}', '{GeneratingRandomValuesForDB.GetRandomUserPassword()}');";
        public static string insertQuery_Дерево = $"INSERT INTO Дерево (\"Назва\") VALUES ('{GeneratingRandomValuesForDB.GetRandomFamilyTreeName()}');";
        public static string insertQuery_Медіа = $"INSERT INTO Медіа (\"Тип_медіа\", \"Шлях_до_файлу\", \"Позначені_особи\", \"Опис\", \"Дата\", \"Місце\", \"Головне_фото\") VALUES ('{GeneratingRandomValuesForDB.GetRandomMediaType()}', '{GeneratingRandomValuesForDB.GetRandomPath()}', {GeneratingRandomValuesForDB.GetRandomNumberOfMarkedPeople()}, '{GeneratingRandomValuesForDB.GetRandomDescription()}', '{GeneratingRandomValuesForDB.GetRandomDate()}', '{GeneratingRandomValuesForDB.GetRandomPlace()}', {GeneratingRandomValuesForDB.MainPhoto()});";
        public static string insertQuery_Особа = $"INSERT INTO Особа (\"Головна_особа\", \"Стать\", \"Прізвище\", \"Дівоче_прізвище\", \"Імя\", \"Інші_варіанти_імені\", \"Дата_народження\", \"Дата_смерті\") VALUES ({GeneratingRandomValuesForDB.isMainPerson()}, '{GeneratingRandomValuesForDB.GetRandomGender()}', '{GeneratingRandomValuesForDB.lastName()}', '{GeneratingRandomValuesForDB.maidenName()}', '{GeneratingRandomValuesForDB.firstName()}', '{GeneratingRandomValuesForDB.GetRandomNameVariants()}', '{GeneratingRandomValuesForDB.GenerateRandomDeathOrBirthDate()}', '{GeneratingRandomValuesForDB.GenerateRandomDeathOrBirthDate()}');";





        // SELECT Queries
        public static string selectQuery_Дерево = "SELECT * FROM \"Дерево\"";   
        public static string selectQuery_Користувач = "SELECT * FROM \"Користувач\"";
        public static string selectQuery_Медіа = "SELECT * FROM \"Медіа\"";
    }
}
