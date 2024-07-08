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

            string[] firstNames = { "����", "����", "������", "�������", "�������", "������", "����", "������", "���������", "������" };
            string[] lastNames = { "������", "������", "�������", "��������", "��������", "��������", "������", "�������", "�����������", "��������" };
            
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