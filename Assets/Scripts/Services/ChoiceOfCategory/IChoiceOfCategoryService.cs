using Scripts.Data;
using System.Collections.Generic;

namespace Scripts.Services
{
    public interface IChoiceOfCategoryService : IService
    {
        public void Create(List<string> list, MainMenuTypes menuType);
        public void CreateForStatistic(List<StatisticData> list);
        public void ButtonPressed(int index);

    }
}