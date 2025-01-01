using Scripts.UI.Markers;
using Scripts.Data;
using Scripts.MyTools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services.Statistics
{
    public class StatisticsInputService : IStatisticsInputService
    {
        private readonly ButtonCreator _buttonCreator;

        private readonly Text _fText;
        private readonly Text _vText;
        private readonly Button _AddButton;
        private readonly InputField _AddInput;

        private readonly Transform _content;

        private StatisticData _currentStatisticData = new StatisticData();

        public StatisticsInputService(StatisticsInputElements statisticsInputElements)
        {
            _fText = statisticsInputElements.fText;
            _vText = statisticsInputElements.vText;
            _AddButton = statisticsInputElements.AddButton;
            _AddInput = statisticsInputElements.AddInput;
        }

        public void ShowPanel(StatisticData data)
        {
            Clear();

            _AddButton.onClick.AddListener(Addation);

            _currentStatisticData = data;
            _fText.text = $"F = {System.Math.Round(_currentStatisticData.F, 3).ToString()}";
            _vText.text = $"V = {System.Math.Round(_currentStatisticData.V, 3).ToString()}";

            ShowData(data.PartCounter);
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