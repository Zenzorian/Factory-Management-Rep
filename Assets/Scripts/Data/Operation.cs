using FactoryManager.Data.Tools;

namespace FactoryManager.Data
{
    public class Operation : TableItem
    {
        public string Name { get; set; }
        public Tool[] Tools { get; set; }
        public override string Type { get; set; }

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