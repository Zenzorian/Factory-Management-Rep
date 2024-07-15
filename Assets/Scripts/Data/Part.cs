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
        public Operation[] Operations;       
        public string Statistics;
        public Part()
        {

        }
        public Part(string name, string partType, Operation[] operations, string statistics)
        {
            Name = name;
            Type = partType;
            Operations = operations;           
            Statistics = statistics;
        }
    }    
}