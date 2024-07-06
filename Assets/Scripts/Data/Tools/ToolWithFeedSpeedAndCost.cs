namespace FactoryManager.Data.Tools
{
    public abstract class ToolWithFeedSpeedAndCost : Tool, IToolWithFeedAndSpeed, IToolWithCost
    {
        public double FMin { get; set; }
        public double FMax { get; set; }
        public double VMin { get; set; }
        public double VMax { get; set; }
        public decimal Cost { get; set; }
        protected ToolWithFeedSpeedAndCost(string marking, double fMin, double fMax, double vMin, double vMax, decimal cost, string note) : base(marking, note)
        {
            FMin = fMin;
            FMax = fMax;
            VMin = vMin;
            VMax = vMax;
            Cost = cost;
        }


    }

}