using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FactoryManager.Data;
using FactoryManager.Data.Tools;
using UnityEngine;

namespace FactoryManager
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        private const string fileName = "FactoryManagerGlobalData.json";
        private string filePath;

        [SerializeField] private GlobalData _globalData;

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, fileName);
#else
            filePath = Path.Combine(Application.dataPath, fileName);
#endif
            LoadData();

            if (instance == null) instance = this;
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
        public void AddWorker(Worker worker)
        {
            _globalData.listOfWorkers.Add(worker);
        }        
        public void AddWorkstation(Workstation workstation)
        {
            _globalData.listOfWorkstations.Add(workstation);
        }
        public void AddPart(Part part)
        {
            _globalData.listOfParts.Add(part);
        } 
        public void AddTool(Tool tool)
        {
            _globalData.listOfTools.Add(tool);
        }
        public int GetItemsCount(MainMenuTypes menuType, string type = null)
        {              
            var menuTypeToListMap = new Dictionary<MainMenuTypes, IEnumerable<TableItem>>
            {
                { MainMenuTypes.Workstations, _globalData.listOfWorkstations },
                { MainMenuTypes.Tools, _globalData.listOfTools },
                { MainMenuTypes.Workers, _globalData.listOfWorkers },
                { MainMenuTypes.Parts, _globalData.listOfParts },                 
                { MainMenuTypes.StatisticPart, _globalData.listOfParts},
                { MainMenuTypes.StatisticTool, _globalData.listOfTools}
            };

            if (!menuTypeToListMap.TryGetValue(menuType, out var list))
            {
                throw new ArgumentException("Invalid MainMenuType provided");
            }            
            if(type!=null)
                return list?.Where(item => item.Type == type).Count() ?? 0;
            else
                return list.Count();
        }

    }
}