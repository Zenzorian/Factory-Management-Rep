namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// ������� ����� ��� ������� ������������
    /// </summary>
    public abstract class Tool
    {
        public string Marking { get; set; }
        public string Note { get; set; }
        public MachineTool ToolType { get; set; }
        public Tool(string marking, string note, MachineTool toolType)
        {
            Marking = marking;
            Note = note;
            ToolType = toolType;
        }
    }
    public interface IToolWithFeedAndSpeed
    {
        double FMin { get; set; }
        double FMax { get; set; }
        double VMin { get; set; }
        double VMax { get; set; }
    }
    public enum MachineTool
    {
        LatheCNC,             // �������� ���
        MillingCNC,           // ��������� ���
        Cutting,              // �������� ������
        Grooving,             // ���������� ������
        ThreadingMachines,    // ��������� ������
        Drills,               // �����
        MillingCutters,       // �����
        Taps,                  // �������
        Other
    }
    public interface IToolWithCost
    {
        decimal Cost { get; set; }
    }
    public enum MeasurementSystem
    {
        Metric,
        Imperial

    }
    public enum CNCMillingToolType
    {
        Rough,
        Bottoming
    }
}