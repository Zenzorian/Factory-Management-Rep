using Scripts.Data;

namespace Scripts.Services
{
    public interface IStatisticsInputService : IService
    {
        void ShowPanel(StatisticData data);
    }
}