using Scripts.Data;

namespace Scripts.Services
{
    public interface IStatisticsGraphViewService : IService
    {
        void Initialize(Statistic statistics);

    }
}