using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{   
    /// <summary>
    /// Рабочий участок
    /// </summary>
    [System.Serializable]
    public class Workstation : TableItem
    {
        public FactoryWorkspace WorkspaceType { get; set; }
        public Tool[] Tools { get; set; }
        public int MaxWorkers { get; set; }
        public int ReservedWorkers { get; set; }
        public Workstation()
        { }
        public Workstation(FactoryWorkspace type,Tool[] tools,int maxWorkers, int reservedWorkers)
        {
            WorkspaceType = type;
            Tools = tools;
            MaxWorkers = maxWorkers;
            ReservedWorkers = reservedWorkers;
        }
    }
    public enum FactoryWorkspace
    {
        AssemblyLine,
        CNC,
        Painting,
        QualityControl,
        Packaging,
        Maintenance,
        Storage,
        Welding,
        Inspection,
        ResearchAndDevelopment
    }
}