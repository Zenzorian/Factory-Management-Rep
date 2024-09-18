using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FactoryManager.Data;
using UnityEngine;

namespace FactoryManager
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager _instance;
        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<DataManager>();
                    if (_instance == null)
                    {
                        var go = new GameObject("DataManager");
                        _instance = go.AddComponent<DataManager>();
                    }
                }
                return _instance;
            }
        }
        private const string fileName = "FactoryManagerGlobalData.json";
        private string filePath;        
        [SerializeField] private GlobalData _globalData;
        public GlobalData GlobalData{get; private set;}

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, fileName);
#else
            filePath = Path.Combine(Application.dataPath, fileName);
#endif
            LoadData();            
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(_globalData, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Data saved to " + filePath);
        }

        public void LoadData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, _globalData);
                Debug.Log("Data loaded from " + filePath);
            }
            else
            {
                Debug.LogWarning("Save file not found at " + filePath);
            }
        }
        public List<string> GetTypesOfWorkers()
        {
            return _globalData.typesOfWorkers;
        }
        public List<string> GetTypesOfWorkstation()
        {
            return _globalData.typesOfWorkstation;
        }
        public List<string> GetTypesOfTools()
        {
            return _globalData.typesOfTools;
        }
        public List<string> GetTypesOfParts()
        {
            return _globalData.typesOfParts;
        }
        public void AddItem(MainMenuTypes menuType, TableItem item)
        {
            switch (menuType)
            {
                case MainMenuTypes.Workstations:                    
                    var workstation = new Workstation(item.Id, item.Name, item.Type);
                    _globalData.listOfWorkstations.Add(workstation);
                    break;
                    
                case MainMenuTypes.Tools:                    
                    var tool = new Tool(item.Id, item.Name, item.Type);
                    _globalData.listOfTools.Add(tool);
                    break;

                case MainMenuTypes.Workers:                    
                    var worker = new Worker(item.Id, item.Name, item.Type);
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
                case MainMenuTypes.Workstations:
                    return _globalData.listOfWorkstations;
                case MainMenuTypes.Tools:
                    return _globalData.listOfTools;
                case MainMenuTypes.Workers:
                    return _globalData.listOfWorkers;
                case MainMenuTypes.Parts:
                    return _globalData.listOfParts;
                case MainMenuTypes.StatisticPart:
                    return _globalData.listOfParts;
                case MainMenuTypes.StatisticTool:
                    return _globalData.listOfTools;
                default:
                    Debug.LogWarning($"Unhandled MainMenuType: {menuType}");
                    return null;
            }
        }
    }
}