namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Мечик
    /// </summary>
    [System.Serializable]
    public class TapTool : Tool
    {
        public MeasurementSystem Measurement;
        public double Pitch;
        public double VMin;
        public double VMax;

        public TapTool(string marking, MeasurementSystem measurement, double pitch, double vMin, double vMax, string note, string type)
            : base(marking, note, type)
        {
            Measurement = measurement;
            Pitch = pitch;
            VMin = vMin;
            VMax = vMax;
        }
    }

}