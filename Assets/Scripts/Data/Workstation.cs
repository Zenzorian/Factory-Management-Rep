using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{   
    /// <summary>
    /// Рабочий участок
    /// </summary>
    [System.Serializable]
    public class Workstation : TableItem
    {
        public Tool[] Tools;
        public int MaxWorkers;
        public int ReservedWorkers;
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