namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// ��������� ��� ������
    /// </summary>
    [System.Serializable]
    public class CNCMillingTool : ToolWithFeedSpeedAndCost
    {
        public CNCMillingToolType CNCMillingToolType;

        public CNCMillingTool(string marking, string note, string type, ManufacturersRecomendedParametrs manufacturerRecommendedSettings, decimal cost)
            : base(marking, note, type, manufacturerRecommendedSettings, cost)
        {
        }
    }
}