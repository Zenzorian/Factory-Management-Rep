using System.Collections.Generic;

namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public abstract class ToolWithFeedSpeedAndCost : Tool, IToolWithFeedAndSpeed, IToolWithCost
    {
        public List<ToolStatistic> ToolStatistics { get; set; }
        public ToolStatistic ManufacturerRecommendedSettings { get; set; }
                
        public decimal Cost { get; set; }
        protected ToolWithFeedSpeedAndCost(string marking, string note, string type, ToolStatistic manufacturerRecommendedSettings, decimal cost) : base(marking, note,type)
        {                      
            Cost = cost;
        }


    }

}