using Scripts.UI.Markers;
using Scripts.Data;
using Scripts.MyTools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Infrastructure.AssetManagement;

namespace Scripts.Services.Statistics
{
    public class StatisticsInputService : IStatisticsInputService
    {
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly IButtonCreator _buttonCreator;
        private readonly IPopUpService _popupService;
        private readonly GlobalUIElements _globalUIElements;

        private readonly Transform _panel;
        private readonly Transform _content;

        private readonly Text _fText;
        private readonly Text _vText;

        private readonly Button _editButton;
        private readonly Button _addButton;
        private readonly Button _backButton;
        private readonly InputField _addInput;      

        private StatisticData _currentStatisticData = new StatisticData();

        private List<Button> _deleteButtons = new List<Button>();
        private bool _isEdit = false;

        public StatisticsInputService
        (
            ISaveloadDataService saveloadDataService,
            IButtonCreator buttonCreator, 
            IPopUpService popupService,
            StatisticsInputElements statisticsInputElements,
            GlobalUIElements globalUIElements
        )
        {
            _saveloadDataService = saveloadDataService;
            _buttonCreator = buttonCreator;
            _popupService = popupService;
            _globalUIElements = globalUIElements;

            _panel = statisticsInputElements.gameObject.GetComponent<Transform>();
            _content = statisticsInputElements.content;

            _fText = statisticsInputElements.fText;
            _vText = statisticsInputElements.vText;

            _addInput = statisticsInputElements.addInput;

            _addButton = statisticsInputElements.addButton;
            _backButton = statisticsInputElements.backButton;           
            _editButton = statisticsInputElements.editButton;
        }

        public void ShowPanel(StatisticData data)
        {
            Clear();

            _currentStatisticData = data;
            _fText.text = $"F = {System.Math.Round(_currentStatisticData.F, 3).ToString()}";
            _vText.text = $"V = {System.Math.Round(_currentStatisticData.V, 3).ToString()}";
                       
            ShowData(_currentStatisticData.PartCounter);

            RegisterEvents();

            _panel.gameObject.SetActive(true);
        }
        public void HidePanel()
        {
            Clear();
            AnregisterEvents();
            _panel.gameObject.SetActive(false);
        }
        private void RegisterEvents()
        {
            _addButton.onClick.AddListener(Addation);
            _backButton.onClick.AddListener(HidePanel);
            _editButton.onClick.AddListener(Edit);

            _globalUIElements.addationButton.gameObject.SetActive(false);
            _globalUIElements.backButton.gameObject.SetActive(false);
            _globalUIElements.editButton.gameObject.SetActive(false);
        }
        private void AnregisterEvents()
        {
            _addButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _editButton.onClick.RemoveAllListeners();

            _globalUIElements.addationButton.gameObject.SetActive(true);
            _globalUIElements.backButton.gameObject.SetActive(true);
            _globalUIElements.editButton.gameObject.SetActive(true);
        }
        private void ShowData(List<int> data)
        {           
            string[] names = new string[data.Count];
            
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = $"{data[i]}";
            }
            CreateButtons(names);
        }
        private void CreateButtons(string[] names)
        {
            _deleteButtons.Clear();
            _isEdit = false;

            var buttons = _buttonCreator.Create(names, _content);

            for(int i = 0; i < buttons.Count; i++)
            {
                var deleteButton = _buttonCreator.CreateDeleteButton(buttons[i].transform);
                _deleteButtons.Add(deleteButton);
                int currentIndex = i;
                deleteButton.onClick.AddListener(() => OnDelete(currentIndex));
                deleteButton.gameObject.SetActive(false);
            }
        }
        private void Addation()
        {
            if (string.IsNullOrEmpty(_addInput.text)) return;

            var data = int.Parse(_addInput.text);
            _currentStatisticData.PartCounter.Add(data);
            _addInput.text = "";

            _saveloadDataService.SaveData();

            ShowPanel(_currentStatisticData);
        }
        private void Edit()
        {
            Debug.Log("Edit" + _isEdit);
            _isEdit = !_isEdit;
            foreach (var button in _deleteButtons)
            {
                if (button == null)return;                
                button.gameObject.SetActive(_isEdit);                
            }
        }
        private void OnDelete(int index)
        {
            _isEdit = false;
            _popupService.ShowConfirm("Are you sure you want to delete this part counter?", () => Delete(index));
        }
        private void Delete(int index)
        {
            _saveloadDataService.DeletePartCounter(_currentStatisticData, index);
            ShowPanel(_currentStatisticData);
        }   
        public void Clear()
        {
            AnregisterEvents(); 
            foreach (Transform item in _content)
            {
                GameObject.Destroy(item.gameObject);
            }
            _deleteButtons.Clear();
        }
    }
}