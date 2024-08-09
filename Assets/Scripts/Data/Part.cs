using FactoryManager.Data.Tools;
using System.Collections.Generic;

namespace FactoryManager.Data
{
    /// <summary>
    /// Желаемая деталь
    /// </summary>
    [System.Serializable]
    public class Part : TableItem
    {
        public string Name;        
        public List<Statistics> Statistics = new List<Statistics>();
        public Part()
        {

        }
        public Part(string name, string partType)
        {
            Name = name;
            Type = partType;            
        }
    }
    [System.Serializable]
    public class Statistics
    {
        public Statistics(Tool tool, ProcessingType processingType)
        {
            Tool = tool;
            ProcessingType = processingType;           
        }

        public Tool Tool;
        public ProcessingType ProcessingType;
        public List<StatisticData> Data = new List<StatisticData>();
    }
    [System.Serializable]
    public enum ProcessingType
    {
        NotSpecified,
        Finishing, 
        Roughing       
    }
    [System.Serializable]
    public class StatisticData
    {
        public double F;
        public double V;
        public List<int> PartCounter = new List<int>();

    }
}