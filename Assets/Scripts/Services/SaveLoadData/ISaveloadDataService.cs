using Scripts.Data;
using System.Collections.Generic;

namespace Scripts.Services
{
    public interface ISaveloadDataService : IService
    {
        void LoadData();
        void SaveData();

        List<string> GetTypesOfItemsListByType(MainMenuTypes menuType);
        IEnumerable<TableItem> GetItemsListByType(MainMenuTypes menuType);

        void AddItem(MainMenuTypes menuType, TableItem item);
        int GetItemsCount(MainMenuTypes menuType, string type = null);
        List<TableItem> GetItemsListWithFilter(MainMenuTypes menuType, int indexOfSelectedCategoty);
    }
}