using Scripts.Data;
using System;
using System.Collections.Generic;

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
             Part part = null
        )
        {
            this.menuType = mainMenuType;
            this.indexOfSelectedCategory = indexOfSelectedCategory;
           
            this.statisticsData = statisticsData;
            this.part = part;
        }
        public MainMenuTypes menuType;
        public int indexOfSelectedCategory;        
        public List<StatisticData> statisticsData;
        public Part part;
    }
}