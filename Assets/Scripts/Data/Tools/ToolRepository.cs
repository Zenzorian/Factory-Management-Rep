using System.Collections.Generic;

namespace FactoryManager.Data.Tools
{
    public class ToolRepository
    {
        private List<Tool> tools = new List<Tool>();

        public void AddTool(Tool tool)
        {
            tools.Add(tool);
        }

        public IEnumerable<Tool> GetAllTools()
        {
            return tools;
        }
    }
}