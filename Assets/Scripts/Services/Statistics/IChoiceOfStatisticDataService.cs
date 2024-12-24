using Scripts.Data;
using Scripts.Infrastructure.States;
using System.Collections.Generic;

namespace Scripts.Services.Statistics
{
    public interface IChoiceOfStatisticDataService : IService
    {
        void ShowPanel(IStateMachine stateMachine, SelectedStatisticData selectedStatisticData = null);
        void HidePanel();      
        void SetProcessingType(ProcessingType processingType);
        bool HasValidStatistics();
        Statistic GetCurrentStatistics();       
    }
}