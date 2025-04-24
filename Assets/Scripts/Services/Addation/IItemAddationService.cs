using Scripts.Data;
using System;
using System.Collections.Generic;
using Scripts.Infrastructure.States;

namespace Scripts.Services
{
    public interface IItemAddationService : IService
    {
        void Open(AddationData addationData, Action OnAdded);
    }
    public class AddationData
    {
        public AddationData
        (
            MainMenuTypes mainMenuType,
            int indexOfSelectedCategory, 
            List<StatisticData> statisticsData = null,
            SelectedStatisticsContext selectedStatistic = null,
                 
            Action onToolButtonClicked = null
        )
        {
            this.menuType = mainMenuType;
            this.indexOfSelectedCategory = indexOfSelectedCategory;
           
            this.statisticsData = statisticsData;
            this.selectedStatistic = selectedStatistic;          
            this.onToolButtonClicked = onToolButtonClicked;
        }
        public MainMenuTypes menuType;
        public int indexOfSelectedCategory;        
        public List<StatisticData> statisticsData;
        public SelectedStatisticsContext selectedStatistic;        
        public Action onToolButtonClicked;
    }
}