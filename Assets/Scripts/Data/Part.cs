using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{
    /// <summary>
    /// Желаемая деталь
    /// </summary>
    [System.Serializable]
    public class Part : TableItem
    {
        public string Name { get; set; }
        public override string Type { get; set; }
        public Operation[] Operations { get; set; }        
        public string Statistics { get; set; }
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