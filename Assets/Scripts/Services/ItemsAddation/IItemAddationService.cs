using System;

namespace Scripts.Services
{
    public interface IItemAddationService : IService
    {
        void Open(AddationData addationData, Action OnAdded);
    }
    public struct AddationData
    {
        public AddationData(MainMenuTypes mainMenuType, int indexOfSelectedCategory, bool itsStatistick)
        {
            this.menuType = mainMenuType;
            this.indexOfSelectedCategory = indexOfSelectedCategory;
            this.itsStatistick = itsStatistick;
        }
        public MainMenuTypes menuType;
        public int indexOfSelectedCategory;
        public bool itsStatistick;
    }
}