namespace FactoryManager.Data.Tools
{   
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

        public ThreadingTool(int id, string name, LocationType location, MeasurementSystem measurement, double vMin, double vMax, double pitch, string note, string type)
            : base(id,name, note, type)
        {
            Location = location;
            Measurement = measurement;
            VMin = vMin;
            VMax = vMax;
            Pitch = pitch;
        }
    }
}