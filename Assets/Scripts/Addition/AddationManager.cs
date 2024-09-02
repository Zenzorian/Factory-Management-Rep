using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FactoryManager
{
    public class AddationManager : MonoBehaviour
    {
        public static AddationManager instance;
        public UnityEvent OnAdded;
        public int typeValue;

        [SerializeField] private Transform _content;
        [SerializeField] private InputFieldCreator _inputFieldCreator = new InputFieldCreator();
        private ChioceListAddation _chioceListAddation = new ChioceListAddation();
        private TableItemAddation _tableItemAddation = new TableItemAddation();
        private StatisticDataItemAddation _statisticDataItemAddation = new StatisticDataItemAddation();
        [SerializeField] private Button _addButton;

       
        private void Awake()
        {
            if (instance == null)
                instance = this;

            _chioceListAddation.Init(_inputFieldCreator, _content);
            _tableItemAddation.Init(_inputFieldCreator, _content);
            _statisticDataItemAddation.Init(_inputFieldCreator, _content);
        }

        public void AddChioceList()
        {
            if (MenuManager.instance.menuType == MainMenuTypes.StatisticTool)
            {
                var list = StatisticsPanelController.CurrentStatisticDataList;
                Debug.Log(list);
                _statisticDataItemAddation.SetStatistic(list, _addButton);
            }
            else
            _chioceListAddation.Set(ChoiceOfCategoryMenu.MenuType, _addButton);
        }
       
        public void AddTableItem()
        {
            _tableItemAddation.Set(ChoiceOfCategoryMenu.MenuType, _addButton, typeValue);           
        }       
    }
}
