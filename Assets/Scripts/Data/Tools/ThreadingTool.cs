namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Резьбовой инструмент
    /// </summary>
    public class ThreadingTool : Tool
    {
        public enum LocationType { External, Internal }
        public LocationType Location { get; set; }
        public MeasurementSystem Measurement { get; set; }
        public double VMin { get; set; }
        public double VMax { get; set; }
        public double Pitch { get; set; }

        public ThreadingTool(string marking, LocationType location, MeasurementSystem measurement, double vMin, double vMax, double pitch, string note,MachineTool type)
            : base(marking, note, type)
        {
            Location = location;
            Measurement = measurement;
            VMin = vMin;
            VMax = vMax;
            Pitch = pitch;
        }
    }
}