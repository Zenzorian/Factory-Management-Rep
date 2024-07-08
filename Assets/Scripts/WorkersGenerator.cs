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
            
            for (int i = 1; i <= numberOfWorkers; i++)
            {
                workers.Add(new Worker(
                    i,
                    firstNames[rnd.Next(firstNames.Length)],
                    lastNames[rnd.Next(lastNames.Length)],
                    (FactoryWorker)UnityEngine.Random.Range(0, 10)
                ));
            }

            return workers;
        }
    }
}