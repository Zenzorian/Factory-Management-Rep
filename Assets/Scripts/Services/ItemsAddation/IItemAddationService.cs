
using System;
using Unity.Mathematics;

namespace Scripts.Services
{
    public interface IItemAddationService : IService
    {
        void Open(AddationData addationData, Action OnAdded);
    }
    public struct AddationData
    {
        public AddationData(MainMenuTypes mainMenuType, string categoryName, bool itsStatistick)
        {
            this.menuType = mainMenuType;
            this.categoryName = categoryName;
            this.itsStatistick = itsStatistick;
        }
        public MainMenuTypes menuType;
        public string categoryName;
        public bool itsStatistick;
    }
}