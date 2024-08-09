namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Инструмент для обработки канавок
    /// </summary>
    [System.Serializable]
    public class GroovingTool : ToolWithFeedSpeedAndCost
    {
        public double Width;

        public GroovingTool(string marking, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, double width, decimal cost)
            : base(marking, note, type, manufacturerRecommendedSettings, cost)
        {
            Width = width;
        }
    }
}