using System;
using Npgsql;

namespace FamilyTree_DB_Migration_Aattempt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (int i = 0; i < 30; i++)
            {
                //Queries.insertQuery_Дерево();
                //Queries.InsertQuery_Користувач();
                //Queries.InsertQuery_Медіа();
                //Queries.InsertQuery_Особа();
                //Queries.InsertQuery_Подія();
                //Queries.InsertQuery_Спеціальний_запис();
                //Queries.InsertQuery_Дерево_Особа();
                //Queries.InsertQuery_Медіа_Особа();
                //Queries.InsertQuery_Медіа_Подія();
                //Queries.InsertQuery_Користувач_Дерево();
                //Queries.InsertQuery_Звязок();
            }
            //SELECT all tables
            //Console.WriteLine("Дерево: ");
            //Queries.SelectДерево();
            //Console.WriteLine("Користувач: ");
            //Queries.SelectКористувач();
            //Console.WriteLine("Медіа: ");
            //Queries.SelectМедіа();
            //Console.WriteLine("Особа: ");
            //Queries.SelectОсоба();
            //Console.WriteLine("Подія: ");
            //Queries.SelectПодія();
            //Console.WriteLine("Спеціальний_запис: ");
            //Queries.SelectСпеціальний_запис();
            //Console.WriteLine("Дерево_Особа: ");
            //Queries.SelectДерево_Особа();
            //Console.WriteLine("Медіа_Особа: ");
            //Queries.SelectМедіа_Особа();
            //Console.WriteLine("Медіа_Подія: ");
            //Queries.SelectМедіа_Подія();
            //Console.WriteLine("Користувач_Дерево: ");
            //Queries.SelectКористувач_Дерево();
            //Console.WriteLine("Звязок: ");
            //Queries.SelectЗвязок();

            // DELETE all from all the tables

            //Queries.DELETE_ALL_FROM_Дерево_Query();
            //Queries.DELETE_ALL_FROM_Дерево_Особа_Query();
            //Queries.DELETE_ALL_FROM_Звязок_Query();
            //Queries.DELETE_ALL_FROM_Користувач_Query();
            //Queries.DELETE_ALL_FROM_Користувач_Дерево_Query();
            //Queries.DELETE_ALL_FROM_Медіа_Query();
            //Queries.DELETE_ALL_FROM_Медіа_Особа_Query();
            //Queries.DELETE_ALL_FROM_Медіа_Подія_Query();
            //Queries.DELETE_ALL_FROM_Особа_Query();
            //Queries.DELETE_ALL_FROM_Подія_Query();
            //Queries.DELETE_ALL_FROM_Спеціальний_запис_Query();
            //Console.WriteLine("Дерево: ");
            //Queries.SelectДерево();

            //SELECT all tables
            Console.WriteLine("Користувач: ");
            Queries.SelectКористувач();
            Console.WriteLine("Медіа: ");
            Queries.SelectМедіа();
            Console.WriteLine("Особа: ");
            Queries.SelectОсоба();
            Console.WriteLine("Подія: ");
            Queries.SelectПодія();
            Console.WriteLine("Спеціальний_запис: ");
            Queries.SelectСпеціальний_запис();
            Console.WriteLine("Дерево_Особа: ");
            Queries.SelectДерево_Особа();
            Console.WriteLine("Медіа_Особа: ");
            Queries.SelectМедіа_Особа();
            Console.WriteLine("Медіа_Подія: ");
            Queries.SelectМедіа_Подія();
            Console.WriteLine("Користувач_Дерево: ");
            Queries.SelectКористувач_Дерево();
            Console.WriteLine("Звязок: ");
            Queries.SelectЗвязок();
        }
    }
}