using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Scripts.Infrastructure;
using System;

namespace Scripts.Services
{
    public class SaveloadDataService:  ISaveloadDataService
    {        
        private const string _fileName = "FactoryManagerGlobalData.json";
        private string _filePath;
        private GlobalData _globalData = new GlobalData();
        private readonly IPopUpService _popupService;
        private readonly ICoroutineRunner _сoroutineRunner;

        public SaveloadDataService(IPopUpService popupService, ICoroutineRunner coroutineRunner)
        {
            _popupService = popupService;
            _filePath = Path.Combine(Application.streamingAssetsPath, $"Data/{_fileName}");
            _сoroutineRunner = coroutineRunner;
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(_globalData, true);
         
            _popupService.ShowMessage("Saving data...", MessageType.message);            
            try
            {
                File.WriteAllText(_filePath, json);
                _popupService.ShowMessageAutoClose("Data saved successfully", MessageType.message);                
            }
            catch (Exception e)
            {
                _popupService.ShowMessageAutoClose("Error saving data", MessageType.error);
                Debug.LogError("Error saving data: " + e.Message);
            }
        }

        public void LoadData()
        { 
            _popupService.ShowMessage("Loading data...", MessageType.message);
            
            UnityWebRequest request = UnityWebRequest.Get(_filePath);
            request.SendWebRequest();
            
            _сoroutineRunner.StartCoroutine(LoadDataCoroutine(request));
        }

        private IEnumerator LoadDataCoroutine(UnityWebRequest request)
        {
            while (!request.isDone)
            {
                yield return null;
            }
            _popupService.CloseMessage();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                JsonUtility.FromJsonOverwrite(json, _globalData);
                _popupService.ShowMessageAutoClose("Data loaded successfully", MessageType.message);
            }
            else
            {
                _popupService.ShowMessageAutoClose("Error loading data", MessageType.error);
            }
        }

        public void AddItemInCategory(MainMenuTypes menuType, string categoryName)
        {
            switch (menuType)
            {
                case MainMenuTypes.Workspaces:
                    _globalData.typesOfWorkspace.Add(categoryName);                   
                    break;
                case MainMenuTypes.Tools:
                    _globalData.typesOfTools.Add(categoryName);
                    break;
                case MainMenuTypes.Workers:
                    _globalData.typesOfWorkers.Add(categoryName);
                    break;
                case MainMenuTypes.Parts:
                    _globalData.typesOfParts.Add(categoryName);
                    break;
                default:
                    Debug.LogWarning("Unsupported MainMenuType");
                    break;
            }
            SaveData();
        }
        public void AddItemInTable(MainMenuTypes menuType, TableItem item)
        {
            switch (menuType)
            {
                case MainMenuTypes.Workspaces:                    
                    var Workspace = new Workspace(item.Id, item.Name, item.Type);
                    _globalData.listOfWorkspaces.Add(Workspace);
                    break;
                    
                case MainMenuTypes.Tools:                    
                    var tool = new Tool(item.Id, item.Name, item.Type);
                    _globalData.listOfTools.Add(tool);
                    break;

                case MainMenuTypes.Workers:                    
                    var worker = new Employee(item.Id, item.Name, item.Type);
                    _globalData.listOfWorkers.Add(worker);
                    break;

                case MainMenuTypes.Parts:                    
                    var part = new Part(item.Id, item.Name, item.Type);
                    _globalData.listOfParts.Add(part);
                    break;
                default:
                    Debug.LogWarning("Unsupported MainMenuType");
                    break;
            }
            SaveData();
        }
        public int GetItemsCount(MainMenuTypes menuType, string type = null)
        {       
            var list = GetItemsListByType(menuType);
            if(type!=null)
                return list?.Where(item => item.Type == type).Count() ?? 0;
            else
                return list.Count();
        }        
        public IEnumerable<TableItem> GetItemsListByType(MainMenuTypes menuType)
        {
            switch (menuType)
            {
                case MainMenuTypes.Workspaces:
                    return _globalData.listOfWorkspaces;
                case MainMenuTypes.Tools:
                    return _globalData.listOfTools;
                case MainMenuTypes.Workers:
                    return _globalData.listOfWorkers;
                case MainMenuTypes.Parts:
                    return _globalData.listOfParts;               
                default:
                    Debug.LogWarning($"Unhandled MainMenuType: {menuType}");
                    return null;
            }
        }
        public List<string> GetTypesOfItemsListByType(MainMenuTypes menuType)
        {
            switch (menuType)
            {
                case MainMenuTypes.Workspaces:
                    return _globalData.typesOfWorkspace;
                case MainMenuTypes.Tools:
                    return _globalData.typesOfTools;
                case MainMenuTypes.Workers:
                    return _globalData.typesOfWorkers;
                case MainMenuTypes.Parts:
                    return _globalData.typesOfParts;               
                default:
                    Debug.LogWarning($"Unhandled MainMenuType: {menuType}");
                    return null;
            }
        }

        public List<TableItem> GetItemsListWithFilter(MainMenuTypes menuType, int indexOfSelectedCategoty)
        {
            List<TableItem> temporaryList = new List<TableItem>();

            string itemType = GetTypesOfItemsListByType(menuType)[indexOfSelectedCategoty];
            IEnumerable<TableItem> ItemList = GetItemsListByType(menuType);
            temporaryList = Filter(itemType, ItemList);

            return temporaryList;
        }
        private List<TableItem> Filter(string type, IEnumerable<TableItem> list)
        {
            var temporaryList = new List<TableItem>();
            foreach (var item in list)
            {
                if (item.Type == type)
                    temporaryList.Add(item);
            }
            return temporaryList;
        }
        public void AddOperation(Part part, string operationName)
        {
            _globalData.listOfParts.Find(p => p.Id == part.Id).Operations.Add(new Operation(operationName));
            SaveData();
        }

        public void DeleteOperation(Part part, string operationName)
        {
            _globalData.listOfParts.Find(p => p.Id == part.Id).Operations.RemoveAll(o => o.Name == operationName);
            SaveData();
        }

        public void AddStatistic(Part part, string operationName, Tool tool, ProcessingType processingType)
        {
            _globalData.listOfParts.Find(p => p.Id == part.Id).Operations.Find(o => o.Name == operationName).Statistics.Add(new Statistic(tool, processingType));
            SaveData();
        }

        public void DeleteStatistic(Part part, Operation operation, Tool tool, ProcessingType processingType)
        {        
            var partOperation = _globalData.listOfParts.Find(p => p.Id == part.Id)
                .Operations.Find(o => o.Name == operation.Name);               
          
            var statistics = partOperation.Statistics;
            for (int i = statistics.Count - 1; i >= 0; i--)
            {
                if (statistics[i].Tool == tool && statistics[i].ProcessingType == processingType)
                {
                    statistics.RemoveAt(i);
                    break; 
                }
            }
            
            SaveData();
        }       

        public void DeleteCategory(MainMenuTypes menuType, int indexOfSelectedCategoty)
        {
            switch (menuType)
            {   
                case MainMenuTypes.Workspaces:
                    _globalData.typesOfWorkspace.RemoveAt(indexOfSelectedCategoty);
                    break;
                case MainMenuTypes.Tools:
                    _globalData.typesOfTools.RemoveAt(indexOfSelectedCategoty);
                    break;
                case MainMenuTypes.Workers:
                    _globalData.typesOfWorkers.RemoveAt(indexOfSelectedCategoty);
                    break;
                case MainMenuTypes.Parts:
                    _globalData.typesOfParts.RemoveAt(indexOfSelectedCategoty);
                    break;
                default:
                    Debug.LogWarning("Unsupported MainMenuType");
                    break;
            }
            SaveData();
        }
    }
}