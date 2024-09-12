namespace FactoryManager.Data.Tools
{    
    [System.Serializable]
    public class GroovingTool : ToolWithFeedSpeedAndCost
    {
        public double Width;

        public GroovingTool(int id, string name, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, double width, decimal cost)
            : base(id, name, note, type, manufacturerRecommendedSettings, cost)
        {
            Width = width;
        }
    }
}