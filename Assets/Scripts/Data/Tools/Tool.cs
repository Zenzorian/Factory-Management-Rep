namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// ������� ����� ��� ������� ������������
    /// </summary>
    public abstract class Tool
    {
        public string Marking { get; set; }
        public string Note { get; set; }

        public Tool(string marking, string note)
        {
            Marking = marking;
            Note = note;
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
        Taps                  // �������
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