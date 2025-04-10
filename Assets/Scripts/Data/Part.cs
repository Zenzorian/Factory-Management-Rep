using System.Collections.Generic;

namespace Scripts.Data
{
    [System.Serializable]
    public class Part : TableItem
    {        
        public List<Operation> Operations;
        
        public Part(int id,string name, string type):base(id,name,type)
        {
            Id = id;    
            Name = name;
            Type = type;
            Operations = new List<Operation>();
        }
    }
    [System.Serializable]
    public class Operation
    {
        public string Name;
        public List<Statistic> Statistics;
        public Operation(string name)
        {
            Name = name;
            Statistics = new List<Statistic>();
        }
    }

    [System.Serializable]
    public class Statistic
    {
        public Tool Tool;
        public ProcessingType ProcessingType;
        public List<StatisticData> Data;
        
        public Statistic(Tool tool, ProcessingType processingType)
        {
            Tool = tool;
            ProcessingType = processingType;
            Data = new List<StatisticData>();
        }
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
        public List<int> PartCounter;
        
        public StatisticData()
        {
            PartCounter = new List<int>();
        }
        public StatisticData(double F, double V)
        {
            this.F = F;
            this.V = V;
            PartCounter = new List<int>(); 
        }
    }
}