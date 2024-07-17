using System.Collections.Generic;

namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Базовый класс для содания инструментов
    /// </summary>
    [System.Serializable]
    public abstract class Tool : TableItem
    {
        public string Marking { get; set; }
        public string Note { get; set; }        
        public Tool(string marking, string note, string toolType)
        {
            Marking = marking;
            Note = note;
            Type = toolType;
        }
    }
    public interface IToolWithFeedAndSpeed
    {
        ManufacturersRecomendedParametrs ManufacturerRecommendedSettings { get; set; }
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
    [System.Serializable]
    public class ManufacturersRecomendedParametrs
    {
        double FMin { get; set; }
        double FMax { get; set; }
        double VMin { get; set; }
        double VMax { get; set; }

        int PartCount { get; set; }

        public ManufacturersRecomendedParametrs(double fMin, double fMax, double vMin, double vMax, int partCount)
        {
            FMin = fMin;
            FMax = fMax;
            VMin = vMin;
            VMax = vMax;
        }       
    }
}