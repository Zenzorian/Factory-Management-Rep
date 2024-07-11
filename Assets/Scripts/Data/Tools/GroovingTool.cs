namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Инструмент для обработки канавок
    /// </summary>
    public class GroovingTool : ToolWithFeedSpeedAndCost
    {
        public double Width { get; set; }

        public GroovingTool(string marking, string note, string type, ToolStatistic manufacturerRecommendedSettings, double width, decimal cost) : base(marking, note, type, manufacturerRecommendedSettings, cost)
        {
            Width = width;           
        }
    }
}