namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Фрезерный ЧПУ станок
    /// </summary>
    public class CNCMillingTool : ToolWithFeedSpeedAndCost
    {
        public CNCMillingToolType CNCMillingToolType { get; set; }


        public CNCMillingTool(string marking, double fMin, double fMax, double vMin, double vMax, decimal cost, string note) : base(marking, fMin, fMax, vMin, vMax, cost, note)
        {

        }
    }
}