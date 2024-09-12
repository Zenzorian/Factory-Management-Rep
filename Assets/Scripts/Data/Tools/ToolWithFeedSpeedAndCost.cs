namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public class ToolWithFeedSpeedAndCost : Tool
    {
        public ManufacturersRecomendedParametrs ManufacturerRecommendedSettings;
        public decimal Cost;

        public ToolWithFeedSpeedAndCost(int id,string name, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, decimal cost)
            : base(id,name, note, type)
        {
            ManufacturerRecommendedSettings = manufacturerRecommendedSettings;
            Cost = cost;
        }
    }

}