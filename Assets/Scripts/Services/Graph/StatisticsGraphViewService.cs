using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Services
{
    public class StatisticsGraphViewService : IStatisticsGraphViewService
    {
        private readonly GraphPlane _graphPlane;
               
        private GameObject _graphObject;
        private List<GraphData> _gpaphDatas = new List<GraphData>();

        public StatisticsGraphViewService(GraphPlane graphPlane)
        {
            _graphPlane = graphPlane;
        }
        public void Initialize(Statistic statistics)
        {
            foreach (var item in statistics.Data)
            {
                var graphData = new GraphData(item.F, item.V, CalculateAverage(item.PartCounter));
                _gpaphDatas.Add(graphData);
            }
            
            _graphPlane.Generate(_gpaphDatas);
        }

        public static float CalculateAverage(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0) return 0;

            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }
            return (float)sum / numbers.Count;
        }
    }
}

