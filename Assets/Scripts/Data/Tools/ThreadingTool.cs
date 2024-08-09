namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Резьбовой инструмент
    /// </summary>
    [System.Serializable]
    public class ThreadingTool : Tool
    {
        [System.Serializable]
        public enum LocationType { External, Internal }

        public LocationType Location;
        public MeasurementSystem Measurement;
        public double VMin;
        public double VMax;
        public double Pitch;

        public ThreadingTool(string marking, LocationType location, MeasurementSystem measurement, double vMin, double vMax, double pitch, string note, string type)
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