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
            string[] positions = { "�������", "�����������", "��������", "��������", "��������", "�����������", "�������������", "HR", "��������", "�����������" };

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