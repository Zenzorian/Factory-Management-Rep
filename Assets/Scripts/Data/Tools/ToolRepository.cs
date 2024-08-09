using System.Collections.Generic;

namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public class ToolRepository
    {
        public List<Tool> tools = new List<Tool>();

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