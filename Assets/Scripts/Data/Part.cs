using FactoryManager.Data.Tools;
using System.Collections.Generic;

namespace FactoryManager.Data
{
    [System.Serializable]
    public class Part : TableItem
    {
        public List<Statistic> Statistic = new List<Statistic>();
        
        public Part(int id,string name, string type):base(id,name,type)
        {
            Id = id;    
            Name = name;
            Type = type;            
        }
    }
    [System.Serializable]
    public class Statistic
    {
        public Statistic(Tool tool, ProcessingType processingType)
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