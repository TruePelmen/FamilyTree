using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree_DB_Migration_Aattempt
{
    class GeneratingRandomValuesForDB
    {
        // TABLE Дерево
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
        // TABLE Користувач
        public static string[] UsersLoginsArray =
        {
            "user123",
            "johndoe45",
            "alice_88",
            "webmaster",
            "cool_gamer",
            "coding_ninja",
            "admin007",
            "user789",
            "developer42",
            "designer123",
            "booklover",
            "coffee_addict",
            "tech_geek",
            "sports_fan",
            "music_lover",
            "movie_buff",
            "traveler_22",
            "foodie",
            "nature_hiker",
            "party_animal",
            "yoga_master",
            "art_enthusiast",
            "history_buff",
            "science_geek",
            "fitness_guru",
            "dog_lover",
            "cat_person",
            "star_wars_fan",
            "startrek_fan",
            "hiking_adventurer"
        };
        public static string[] UsersPasswordsArray ={
            "P@ssw0rd123",
            "SecurePass456",
            "RandomPwd_789",
            "StrongPwd!2022",
            "SecretCode123",
            "Pa$$w0rd!567",
            "SecureAccess99",
            "P@ss123word",
            "Password123!",
            "SecurePwd#2023",
            "RandomPass$123",
            "StrongPassword",
            "SecretKey$789",
            "Pa$$word2022",
            "SecureLogin#99",
            "P@ssw0rd456",
            "Password!123",
            "1234SecurePwd",
            "Strong@2023",
            "Random!Pwd789",
            "P@ssKey!2022",
            "PwdSecure123",
            "Secure#Pass99",
            "12345SecretPwd",
            "StrongPwd$22",
            "Random123!",
            "P@ssw0rd2022",
            "Secure123456",
            "SecretPwd#99",
            "Pa$$2023word",
            "1234StrongPwd"};
        public static string GetRandomUserLogin()
        {
            return UsersLoginsArray[new Random().Next(0, 29)];
        }
        public static string GetRandomUserPassword()
        {
            return UsersPasswordsArray[new Random().Next(0, 29)];
        }
    }
}
