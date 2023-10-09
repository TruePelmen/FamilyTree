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
        // TABLE Медіа
        public static string[] MediaTypeArray = { "зображення", "аудіо", "документ" };
        public static string[] PathesArray = {
            "C:\\Users\\user\\Documents\\report.txt",
            "D:\\Projects\\myapp\\images\\photo.jpg",
            "E:\\Music\\album\\song.mp3",
            "F:\\Photos\\vacation\\beach.jpg",
            "C:\\Downloads\\document.pdf",
            "E:\\Music\\album\\song2.mp3",
            "F:\\Documents\\presentation.pptx",
            "C:\\Photos\\family\\wedding\\photo1.jpg",
            "D:\\Music\\playlist\\song2.mp3",
            "E:\\Projects\\website\\index.html",
            "C:\\Downloads\\document2.pdf",
            "F:\\Photos\\holiday\\scenery.png",
            "E:\\Documents\\resume.docx",
            "D:\\Music\\album2\\track3.mp3",
            "C:\\Users\\user\\Documents\\report3.txt",
            "F:\\Music\\album\\song3.mp3",
            "E:\\Videos\\tutorial.mp4",
            "D:\\Photos\\holiday\\scenery2.png",
            "E:\\Documents\\resume2.docx",
            "F:\\Music\\album2\\track4.mp3",
            "C:\\Users\\user\\Documents\\report4.txt",
            "D:\\Projects\\myapp\\images\\photo2.jpg",
            "E:\\Music\\album\\song4.mp3",
            "F:\\Photos\\vacation\\beach2.jpg",
            "C:\\Downloads\\document3.pdf",
            "E:\\Music\\album\\song5.mp3",
            "F:\\Documents\\presentation2.pptx",
            "C:\\Photos\\family\\wedding2\\photo1.jpg",
            "D:\\Music\\playlist2\\song2.mp3",
            "E:\\Projects\\website3\\index.html",
            "C:\\Downloads\\document4.pdf",
            "F:\\Photos\\holiday2\\scenery.png"
        };
        public static string[] DescriptionsArray = {
            "Фотографія бабусі на весіллі",
            "Родинне фото на дачі",
            "Відео дитинства на морі",
            "Звіт про річний відпочинок",
            "Селфі на Різдвяному ранку",
            "Аудіозапис розмови з батьками",
            "Фото дітей на шкільному випускному",
            "Відео дорожньої подорожі",
            "Фотографія дідуся з тортом",
            "Аудіокнига Улюблена казка",
            "Знімок сімейної зустрічі",
            "Фото мами з внуками",
            "Аудіозапис родинної години",
            "Фотографія народження сестрички",
            "Відео сімейного вечора",
            "Фото батьків з весільного дня",
            "Аудіозапис розмови з дітьми",
            "Фото на випускному в університеті",
            "Відео подорожі на гори",
            "Фотографія брата з першим автомобілем",
            "Селфі на день народження",
            "Фото внука на різдвяному ялинці",
            "Аудіокнига Сімейні рецепти",
            "Знімок батьків на святковому столі",
            "Фото сина на дитячому святі",
            "Відео з весняного відпочинку",
            "Фотографія доньки на весільному дні",
            "Аудіозапис розмови на сімейному пікніку",
            "Фото внучки з випускним альбомом"
};
        public static string[] DatesArray = {
            "2000-01-15",
            "2005-03-22",
            "2010-05-07",
            "2015-07-12",
            "2020-09-25",
            "1999-11-03",
            "2002-04-18",
            "2008-06-30",
            "2013-08-09",
            "2018-10-11",
            "1998-12-05",
            "2004-02-20",
            "2009-04-02",
            "2014-06-15",
            "2019-08-28",
            "1997-10-07",
            "2001-12-29",
            "2006-02-11",
            "2011-04-24",
            "2016-07-05",
            "2021-09-19",
            "1996-03-17",
            "2003-05-01",
            "2007-07-23",
            "2012-09-06",
            "2017-11-18",
            "1995-01-08",
            "2004-07-14",
            "2010-09-26",
            "2016-11-03",
            "2022-01-20"
        };
        public static string[] PlacesArray = {
            "Київ, Україна",
            "Львів, Україна",
            "Одеса, Україна",
            "Харків, Україна",
            "Дніпро, Україна",
            "Тернопіль, Україна",
            "Івано-Франківськ, Україна",
            "Чернівці, Україна",
            "Луцьк, Україна",
            "Рівне, Україна",
            "Житомир, Україна",
            "Полтава, Україна",
            "Суми, Україна",
            "Вінниця, Україна",
            "Запоріжжя, Україна",
            "Миколаїв, Україна",
            "Херсон, Україна",
            "Черкаси, Україна",
            "Хмельницький, Україна",
            "Кривий Ріг, Україна",
            "Севастополь, Україна",
            "Сімферополь, Україна",
            "Донецьк, Україна",
            "Луганськ, Україна",
            "Маріуполь, Україна",
            "Жовква, Україна",
            "Біла Церква, Україна",
            "Краматорськ, Україна",
            "Мукачево, Україна",
            "Слов'янськ, Україна"
        };
        public static string GetRandomMediaType()
        {
            return MediaTypeArray[new Random().Next(0, 2)];
        }
        public static string GetRandomPath()
        {
            return PathesArray[new Random().Next(0, 29)];
        }
        public static int GetRandomNumberOfMarkedPeople()
        {
            return new Random().Next(0, 10);
        }
        public static string GetRandomDescription()
        {
            return DescriptionsArray[new Random().Next(0, 29)];
        }
        public static string GetRandomDate()
        {
            return DatesArray[new Random().Next(0, 29)];
        }
        public static string GetRandomPlace()
        {
            return PlacesArray[new Random().Next(0, 29)];
        }
        public static bool MainPhoto()
        {
            return new Random().Next(2) == 0;
        }

    }
}
