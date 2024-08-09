namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Базовый класс для содания инструментов
    /// </summary>
    [System.Serializable]   
    public class Tool : TableItem
    {
        public string Marking;
        public string Note;

        public Tool(string marking, string note, string type)
        {
            Marking = marking;
            Note = note;
            Type = type;
        }
    }
    [System.Serializable]
    public enum MeasurementSystem
    {
        Metric,
        Imperial
    }
    [System.Serializable]
    public enum CNCMillingToolType
    {
        Rough,
        Bottoming
    }
    [System.Serializable]  
    public class ManufacturersRecomendedParametrs
    {
        public double FMin;
        public double FMax;
        public double VMin;
        public double VMax;
        public int PartCount;

        public ManufacturersRecomendedParametrs(double fMin, double fMax, double vMin, double vMax, int partCount)
        {
            FMin = fMin;
            FMax = fMax;
            VMin = vMin;
            VMax = vMax;
            PartCount = partCount;
        }
    }

}