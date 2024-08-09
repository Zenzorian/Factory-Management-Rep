using FactoryManager.Data;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsGraphView : MonoBehaviour
{
    [SerializeField] private GraphPlane _graphPlane;

    private Statistics _currentStatistic;
    private List<GraphData> _gpaphDatas = new List<GraphData>();
    public void Init(Statistics statistics)
    {
        _currentStatistic = statistics;

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

