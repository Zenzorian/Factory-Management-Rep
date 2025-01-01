using System.Collections.Generic;

namespace Scripts.Data
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
            Data = new List<StatisticData>();
        }

        public Tool Tool;
        public ProcessingType ProcessingType;
        public List<StatisticData> Data;
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
        public StatisticData()
        {
            
        }
        public StatisticData(double F, double V)
        {
            this.F = F;
            this.V = V;
            PartCounter = new List<int>(); 
        }
        public double F;
        public double V;
        public List<int> PartCounter;

    }
}