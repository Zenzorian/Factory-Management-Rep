using Scripts.Data;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Scripts.Services
{
    public interface IChoiceOfCategoryService : IService
    {
        void Activate();
        void Deactivate();
        public void Create(List<string> list, MainMenuTypes menuType);
        public void CreateForStatistic(List<StatisticData> list);
        public void ButtonPressed(int index);
    }
}