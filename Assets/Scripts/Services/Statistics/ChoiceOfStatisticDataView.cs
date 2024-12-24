using UnityEngine.UI;
using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;
using Scripts.Infrastructure.States;
using System;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticDataView
    {
        public Text PartText { get; private set; }
        public Button SelectPartButton { get; private set; }
        public Text ToolText { get; private set; }
        public Button SelectToolButton { get; private set; }
        public Button GoToStatisticsButton { get; private set; }
        public Text ProcessingTypeText { get; private set; }
        public Dropdown ProcessingTypeDropdown { get; private set; }

        private GameObject _statisticPanel;
        public ChoiceOfStatisticDataView(StatisticViewElements elements)
        {
            PartText = elements.partText;
            SelectPartButton = elements.selectPartButton;
            ToolText = elements.toolText;
            SelectToolButton = elements.selectToolButton;
            GoToStatisticsButton = elements.goToStatisticsButton;
            ProcessingTypeText = elements.processingTypeText;
            ProcessingTypeDropdown = elements.processingTypeDropdown;

            Initialize();

            _statisticPanel = elements.gameObject;
        }

        public void ShowPanel(SelectedStatisticData selectedStatisticData)
        {
            _statisticPanel.SetActive(true);

            CheckSelectedData(selectedStatisticData);
        }
        public void HidePanel()
        {
            _statisticPanel.SetActive(false);
        }

        private void CheckSelectedData(SelectedStatisticData selectedStatisticData)
        {
            if (selectedStatisticData.selectedPart == null)
            {
                ShowPartSelection();
            }
            else if (selectedStatisticData.selectedPart != null && selectedStatisticData.selectedTool == null)
            {
                PartText.text = $"Selected: {selectedStatisticData.selectedPart.Id} {selectedStatisticData.selectedPart.Name}";
            }

        }


        private void Initialize()
        {
            GoToStatisticsButton.GetComponentInChildren<Text>().text = "Go to Statistics";

            // Hide initially
            ProcessingTypeText.gameObject.SetActive(false);
            ProcessingTypeDropdown.gameObject.SetActive(false);
            ToolText.gameObject.SetActive(false);
            SelectToolButton.gameObject.SetActive(false);
            GoToStatisticsButton.gameObject.SetActive(false);
        }

        public void ShowPartSelection()
        {
            PartText.text = "Part";
            SelectPartButton.GetComponentInChildren<Text>().text = "Select Part";
        }

        public void ShowProcessingTypeOptions(List<string> options)
        {
            ProcessingTypeDropdown.ClearOptions();
            ProcessingTypeDropdown.AddOptions(options);
            ProcessingTypeDropdown.gameObject.SetActive(true);
        }

        public void ShowToolSelection()
        {
            ToolText.text = "Tool";
            SelectToolButton.GetComponentInChildren<Text>().text = "Select Tool";

            ToolText.gameObject.SetActive(true);
            SelectToolButton.gameObject.SetActive(true);
        }

        public void ShowStatisticsButtons()
        {
            GoToStatisticsButton.gameObject.SetActive(true);
        }
    }
}
