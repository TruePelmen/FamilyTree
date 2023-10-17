using System;
using Npgsql;

namespace FamilyTree_DB_Migration_Aattempt
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //for (int i = 0; i < 30; i++)
            //{
            //    Queries.insertQuery_Дерево();
            //    Queries.InsertQuery_Користувач();
            //    Queries.InsertQuery_Медіа();
            //    Queries.InsertQuery_Особа();
            //    Queries.InsertQuery_Подія();
            //    Queries.InsertQuery_Спеціальний_запис();
            //    Queries.InsertQuery_Дерево_Особа();
            //    Queries.InsertQuery_Медіа_Особа();
            //    Queries.InsertQuery_Медіа_Подія();
            //    Queries.InsertQuery_Користувач_Дерево();
            //    Queries.InsertQuery_Звязок();
            //}
            //Queries.SelectДерево();
            //Queries.SelectКористувач();
            //Queries.SelectМедіа();

            Queries.SelectОсоба();
            //Queries.SelectПодія();
            //Queries.SelectСпеціальний_запис();
            //Queries.SelectДерево_Особа();
            //Queries.SelectМедіа_Особа();
            //Queries.SelectМедіа_Подія();
            //Queries.SelectКористувач_Дерево();
            //Queries.SelectЗвязок();

        }
    }
}
