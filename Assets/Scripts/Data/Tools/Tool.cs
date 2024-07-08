namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Базовый класс для содания инструментов
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
        LatheCNC,             // Токарный ЧПУ
        MillingCNC,           // Фрезерный ЧПУ
        Cutting,              // Отрезные станки
        Grooving,             // Канавочные станки
        ThreadingMachines,    // Резьбовые станки
        Drills,               // Свёрла
        MillingCutters,       // Фрезы
        Taps,                  // Метчики
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