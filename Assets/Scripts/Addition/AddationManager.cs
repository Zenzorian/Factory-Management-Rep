using System.Collections.Generic;
using FactoryManager.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FactoryManager
{
    public class AddationManager : MonoBehaviour
    {
        public static AddationManager instance;
        public UnityEvent OnAdded;
        [SerializeField] private Transform _content;
        [SerializeField] private InputFieldCreator _inputFieldCreator = new InputFieldCreator();
        private ChioceListAddation _chioceListAddation;
        private StatisticDataItemAddation _statisticDataItemAddation;
        [SerializeField] private Button _addButton;

        public void Awake()
        {
            if(instance == null)
            instance = this;

            _chioceListAddation = new ChioceListAddation(_inputFieldCreator, _content, OnAdded,_addButton);            
            _statisticDataItemAddation= new StatisticDataItemAddation(_inputFieldCreator, _content, OnAdded,_addButton);           
        }

        public void Open(MainMenuTypes menuTupe, string TemporaryTableItemType)
        {            
            Debug.Log("Table Item Addation");
            
            _addButton.onClick.RemoveAllListeners();
           
            switch (menuTupe)
            {
                case MainMenuTypes.Workstations:
                    var workstationAddation = new WorkstationAddation(_inputFieldCreator, _content, OnAdded,_addButton,TemporaryTableItemType);                            
                    break;
                case MainMenuTypes.Tools:
                    var toolAddation = new ToolAddation(_inputFieldCreator, _content, OnAdded,_addButton,TemporaryTableItemType);                    
                    break;
                case MainMenuTypes.Workers:
                    var workerAddation = new WorkerAddation(_inputFieldCreator, _content, OnAdded,_addButton,TemporaryTableItemType);          
                    break;
                case MainMenuTypes.Parts:
                    var partAddation = new PartAddation(_inputFieldCreator, _content, OnAdded,_addButton,TemporaryTableItemType);             
                    break;
                default:
                    break;
            }           
        }
        public void Open(List<string> list)
        {
            _chioceListAddation.Open(list); 
            Debug.Log("Chioce List Addation");
        }
        public void Open(List<StatisticData> list)
        {
            _statisticDataItemAddation.Open(list);
            Debug.Log("Statistic Data Item Addation");
        }           
    }
}
