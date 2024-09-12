namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public class TapTool : Tool
    {
        public MeasurementSystem Measurement;
        public double Pitch;
        public double VMin;
        public double VMax;

        public TapTool(int id, string name, MeasurementSystem measurement, double pitch, double vMin, double vMax, string note, string type)
            : base(id,name, note, type)
        {
            Measurement = measurement;
            Pitch = pitch;
            VMin = vMin;
            VMax = vMax;
        }
    }

}