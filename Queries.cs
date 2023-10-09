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







        // SELECT Queries
        public static string selectQuery_Дерево = "SELECT * FROM \"Дерево\"";   
        public static string selectQuery_Користувач = "SELECT * FROM \"Користувач\"";
    }
}
