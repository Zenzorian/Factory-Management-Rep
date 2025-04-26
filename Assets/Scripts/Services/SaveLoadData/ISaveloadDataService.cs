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
        
        void AddItemInCategory(MainMenuTypes menuType, string categoryName);
        void AddItemInTable(MainMenuTypes menuType, TableItem item);

        int GetItemsCount(MainMenuTypes menuType, string type = null);
        List<TableItem> GetItemsListWithFilter(MainMenuTypes menuType, int indexOfSelectedCategoty);

        void AddOperation(Part part, string operationName);
        void DeleteOperation(Part part, string operationName);
        
        void AddStatistic(Part part, string operationName, Tool tool, ProcessingType processingType);
        void DeleteStatistic(Part part, Operation operation, Tool tool, ProcessingType processingType);

        void DeleteCategory(MainMenuTypes menuType, int indexOfSelectedCategoty);
    }
}