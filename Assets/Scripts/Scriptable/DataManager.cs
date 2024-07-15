using System.IO;
using UnityEngine;

namespace FactoryManager
{
    public class DataManager : MonoBehaviour
    {
        private const string fileName = "FactoryManagerGlobalData.json";
        private string filePath;

        [SerializeField] private GlobalData globalData;

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
            string json = JsonUtility.ToJson(globalData, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Data saved to " + filePath);
        }

        public void LoadData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, globalData);
                Debug.Log("Data loaded from " + filePath);
            }
            else
            {
                Debug.LogWarning("Save file not found at " + filePath);
            }
        }
    }
}
