namespace FactoryManager.Data.Tools
{    
    [System.Serializable]
    public class CNCMillingTool : ToolWithFeedSpeedAndCost
    {
        public CNCMillingToolType CNCMillingToolType;

        public CNCMillingTool(int id, string name, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, decimal cost)
            : base(id, name, note, type, manufacturerRecommendedSettings, cost)
        {
        }
    }
}