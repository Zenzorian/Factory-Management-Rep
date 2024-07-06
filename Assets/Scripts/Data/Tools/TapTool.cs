namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Мечик
    /// </summary>
    public class TapTool : Tool
    {
        public MeasurementSystem Measurement { get; set; }
        public double Pitch { get; set; }
        public double VMin { get; set; }
        public double VMax { get; set; }

        public TapTool(string marking, MeasurementSystem measurement, double pitch, double vMin, double vMax, string note)
            : base(marking, note)
        {
            Measurement = measurement;
            Pitch = pitch;
            VMin = vMin;
            VMax = vMax;
        }
    }
}