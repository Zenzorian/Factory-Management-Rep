using Scripts.Data;
using Scripts.Infrastructure.States;
using System.Collections.Generic;

namespace Scripts.Services.Statistics
{
    public interface IChoiceOfStatisticService : IService
    {
        void ShowPanel(IStateMachine stateMachine, SelectedStatisticsContext selectedStatisticData = null);
        void HidePanel();      
        void SetProcessingType(ProcessingType processingType);
        bool HasValidStatistics();
        Statistic GetCurrentStatistic();       
    }
}