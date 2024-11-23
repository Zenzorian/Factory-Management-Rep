using Scripts.Data;
using System.Collections.Generic;

namespace Scripts.Services
{
    public interface ISaveloadDataService : IService
    {
        void LoadData();
        void SaveData();

        List<string> GetTypesOfWorkers();
        List<string> GetTypesOfWorkspace();
        List<string> GetTypesOfTools();
        List<string> GetTypesOfParts();

        void AddItem(MainMenuTypes menuType, TableItem item);
        public int GetItemsCount(MainMenuTypes menuType, string type = null);
        public IEnumerable<TableItem> GetItemsListByType(MainMenuTypes menuType);
    }
}