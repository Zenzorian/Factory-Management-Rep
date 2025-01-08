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

        private readonly GlobalUIElements _globalUIElements;

        private readonly Transform _panel;
        private readonly Transform _content;

        private readonly Text _fText;
        private readonly Text _vText;
        private readonly Button _addButton;
        private readonly Button _backButton;
        private readonly InputField _AddInput;      

        private StatisticData _currentStatisticData = new StatisticData();

        public StatisticsInputService
        (
            ISaveloadDataService saveloadDataService,
            IButtonCreator buttonCreator, 
            StatisticsInputElements statisticsInputElements,
            GlobalUIElements globalUIElements
        )
        {
            _saveloadDataService = saveloadDataService;
            _buttonCreator = buttonCreator;

            _globalUIElements = globalUIElements;

            _panel = statisticsInputElements.gameObject.GetComponent<Transform>();
            _content = statisticsInputElements.content;

            _fText = statisticsInputElements.fText;
            _vText = statisticsInputElements.vText;
            _addButton = statisticsInputElements.addButton;
            _backButton = statisticsInputElements.backButton;
            _AddInput = statisticsInputElements.addInput;
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
            _globalUIElements.addationButton.gameObject.SetActive(false);
            _globalUIElements.backButton.gameObject.SetActive(false);
        }
        private void AnregisterEvents()
        {
            _addButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _globalUIElements.addationButton.gameObject.SetActive(true);
            _globalUIElements.backButton.gameObject.SetActive(true);
        }
        private void ShowData(List<int> data)
        {           
            string[] names = new string[data.Count];
            
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = $"{data[i]}";
            }
            _buttonCreator.Create(names, _content);
        }
        private void Addation()
        {
            if (string.IsNullOrEmpty(_AddInput.text)) return;

            var data = int.Parse(_AddInput.text);
            _currentStatisticData.PartCounter.Add(data);
            _AddInput.text = "";

            _saveloadDataService.SaveData();

            ShowPanel(_currentStatisticData);

        }
        public void Clear()
        {
            foreach (Transform item in _content)
            {
                GameObject.Destroy(item.gameObject);
            }
        }
    }
}