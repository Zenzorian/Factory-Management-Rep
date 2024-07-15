using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{
    public class Operation : TableItem
    {
        public string Name;
        public Tool[] Tools;
        
        public Operation()
        {
            
        }
        public Operation(string name, Tool[] tools )
        {
            Name = name;
            Tools = tools;
        }
        }
}