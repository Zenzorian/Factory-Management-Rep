namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// ��������� ��� ������
    /// </summary>
    public class CNCMillingTool : ToolWithFeedSpeedAndCost
    {
        public CNCMillingToolType CNCMillingToolType { get; set; }

        public CNCMillingTool(string marking, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, decimal cost) : base(marking, note, type,manufacturerRecommendedSettings, cost)
        {

        }
    }
}