namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Инструмент для обработки канавок
    /// </summary>
    public class GroovingTool : ToolWithFeedSpeedAndCost
    {
        public double Width { get; set; }

        public GroovingTool(string marking, double fMin, double fMax, double vMin, double vMax, double width, decimal cost, string note, MachineTool type)
            : base(marking, fMin, fMax, vMin, vMax, cost, note,type)
        {
            Width = width;
        }
    }
}