namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public class ToolWithFeedSpeedAndCost : Tool
    {
        public ManufacturersRecomendedParametrs ManufacturerRecommendedSettings;
        public decimal Cost;

        public ToolWithFeedSpeedAndCost(string marking, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, decimal cost)
            : base(marking, note, type)
        {
            ManufacturerRecommendedSettings = manufacturerRecommendedSettings;
            Cost = cost;
        }
    }

}