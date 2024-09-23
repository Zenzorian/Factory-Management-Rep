namespace FactoryManager.Data
{     
    [System.Serializable]
    public class Workspace : TableItem
    {
        public Tool[] Tools;
        public int MaxWorkers;
        public int ReservedWorkers;
        public Workspace(int id,string name,string type):base(id,name,type)
        {
            Id = id;
            Name = name;            
            Type = type;
        }
        public Workspace(int id,string name,string type,Tool[] tools,int maxWorkers, int reservedWorkers):base(id,name,type)
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