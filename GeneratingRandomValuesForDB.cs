using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree_DB_Migration_Aattempt
{
    class GeneratingRandomValuesForDB
    {
        public static string[] TreeNamesArray = {
            "Сім'я Іванових",
            "Родина Петренків",
            "Домівка Соколових",
            "Гілка Коваленків",
            "Дерево Федорових",
            "Коріння Данилових",
            "Родовід Михайлових",
            "Лінія Савченків",
            "Гілка Тарасенків",
            "Кінець Кузьмінів",
            "Вершина Лисенків",
            "Сім'я Жукових",
            "Родина Морозових",
            "Домівка Шевченків",
            "Гілка Григоренків",
            "Дерево Ковальових",
            "Коріння Яковенків",
            "Родовід Ігнатенків",
            "Лінія Павленків",
            "Гілка Іванченків",
            "Кінець Федосіїв",
            "Вершина Семенків",
            "Сім'я Котенків",
            "Родина Тимошенків",
            "Домівка Сидоренків",
            "Гілка Васильченків",
            "Дерево Полякових",
            "Коріння Горбаченків",
            "Родовід Денисенків",
            "Лінія Олексієнків"
        };
        public static string GetRandomFamilyTreeName()
        {
            return TreeNamesArray[new Random().Next(0, 29)];
        }
    }  
}
