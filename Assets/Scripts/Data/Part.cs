using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{
    /// <summary>
    /// Желаемая деталь
    /// </summary>
    [System.Serializable]
    public class Part : TableItem
    {
        public string Name;        
        public Statistics Statistics;
        public Part()
        {

        }
        public Part(string name, string partType)
        {
            Name = name;
            Type = partType;            
        }
    }
    public class Statistics
    {
        public Tool Tool;
        public ProcessingType ProcessingType;
        public StatisticData[] Data;
    }
    public enum ProcessingType
    {
        Finishing, 
        Roughing,
        NotSpecified
    }
    public class StatisticData
    {
        public double FMin;
        public double VMin;
        public int[] PartCount;

    }
}