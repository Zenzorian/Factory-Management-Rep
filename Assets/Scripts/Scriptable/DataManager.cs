using System.Collections.Generic;
using System.IO;
using FactoryManager.Data;
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
        public List<string> GetTypesOfWorkspaces()
        {
            return _globalData.typesOfWorkspaces;
        }
        public List<string> GetTypesOfTools()
        {
            return _globalData.typesOfTools;
        }
        public void AddWorker(Worker worker)
        {
            _globalData.listOfWorkers.Add(worker);
        }
    }
}