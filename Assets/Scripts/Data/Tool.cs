namespace Scripts.Data
{   
    [System.Serializable]   
    public class Tool : TableItem
    {       
        public string Note;

        public Tool(int id, string name,string type):base(id,name,type)
        {
            Id = id;
            Name = name;            
            Type = type;
        }
        public Tool(int id, string name, string note, string type):base(id,name,type)
        {
            Id = id;
            Name = name;
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