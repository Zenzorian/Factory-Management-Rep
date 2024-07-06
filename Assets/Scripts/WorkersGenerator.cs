using FactoryManager.Data;
using System;
using System.Collections.Generic;

namespace FactoryManager
{
    public static class WorkersGenerator
    {
        public static List<Worker> GenerateWorkers(int numberOfWorkers)
        {
            List<Worker> workers = new List<Worker>();
            Random rnd = new Random();

            string[] firstNames = { "Иван", "Петр", "Сергей", "Алексей", "Дмитрий", "Михаил", "Егор", "Никита", "Александр", "Максим" };
            string[] lastNames = { "Иванов", "Петров", "Сидоров", "Алексеев", "Дмитриев", "Михайлов", "Егоров", "Никитин", "Александров", "Максимов" };
            string[] positions = { "Инженер", "Программист", "Менеджер", "Аналитик", "Дизайнер", "Тестировщик", "Администратор", "HR", "Директор", "Разработчик" };

            //for (int i = 1; i <= numberOfWorkers; i++)
            //{
            //    workers.Add(new Worker(
            //        i.ToString(),
            //        firstNames[rnd.Next(firstNames.Length)],
            //        lastNames[rnd.Next(lastNames.Length)],
            //        positions[rnd.Next(positions.Length)]
            //    ));
            //}

            return workers;
        }
    }
}