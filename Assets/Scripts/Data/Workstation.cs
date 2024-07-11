using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{   
    /// <summary>
    /// Рабочий участок
    /// </summary>
    [System.Serializable]
    public class Workstation : TableItem
    {
        public override string Type { get; set; }
        public Tool[] Tools { get; set; }
        public int MaxWorkers { get; set; }
        public int ReservedWorkers { get; set; }
        public Workstation()
        { }
        public Workstation(string type,Tool[] tools,int maxWorkers, int reservedWorkers)
        {
            Type = type;
            Tools = tools;
            MaxWorkers = maxWorkers;
            ReservedWorkers = reservedWorkers;
        }
    }    
}