using System.Collections.Generic;

namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public abstract class ToolWithFeedSpeedAndCost : Tool, IToolWithFeedAndSpeed, IToolWithCost
    {      
        public ManufacturersRecomendedParametrs ManufacturerRecommendedSettings { get; set; }
                
        public decimal Cost { get; set; }
        protected ToolWithFeedSpeedAndCost(string marking, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, decimal cost) : base(marking, note,type)
        {                      
            Cost = cost;
        }


    }

}