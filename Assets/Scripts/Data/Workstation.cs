namespace FactoryManager.Data
{     
    [System.Serializable]
    public class Workstation : TableItem
    {
        public Tool[] Tools;
        public int MaxWorkers;
        public int ReservedWorkers;
        public Workstation(int id,string name,string type):base(id,name,type)
        {
            Id = id;
            Name = name;            
            Type = type;
        }
        public Workstation(int id,string name,string type,Tool[] tools,int maxWorkers, int reservedWorkers):base(id,name,type)
        {
            Id = id;
            Name = name;
            Type = type;
            Tools = tools;
            MaxWorkers = maxWorkers;
            ReservedWorkers = reservedWorkers;
        }
    }    
}