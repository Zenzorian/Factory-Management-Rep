using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.UI.Markers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticDataView
    {
        public Button SelectPartButton { get; private set; }
        public Dropdown ProcessingTypeDropdown { get; private set; }
        public Button SelectToolButton { get; private set; }

        public Button GoToStatisticsButton { get; private set; }
        public Button EditStatisticsButton { get; private set; }

        private readonly Text _partText;
        private readonly Text _toolText;
        private readonly Text _processingTypeText;

        private GameObject _statisticPanel;
        public ChoiceOfStatisticDataView(StatisticViewElements elements)
        {
            _partText = elements.partText;
            SelectPartButton = elements.selectPartButton;
            _toolText = elements.toolText;
            SelectToolButton = elements.selectToolButton;            
            _processingTypeText = elements.processingTypeText;
            ProcessingTypeDropdown = elements.processingTypeDropdown;

            GoToStatisticsButton = elements.goToStatisticsButton;
            EditStatisticsButton = elements.editStatisticsButton;
                
            _statisticPanel = elements.gameObject;
        }

        public void ShowPanel(SelectedStatisticsContext selectedStatisticData)
        {
            _statisticPanel.SetActive(true);            
        }
        public void HidePanel()
        {
            _statisticPanel.SetActive(false);
        }

        public void Initialize()
        {
            GoToStatisticsButton.GetComponentInChildren<Text>().text = "Go to Statistics";
                       
            _processingTypeText.gameObject.SetActive(false);
            ProcessingTypeDropdown.gameObject.SetActive(false);
            _toolText.gameObject.SetActive(false);
            SelectToolButton.gameObject.SetActive(false);
            GoToStatisticsButton.gameObject.SetActive(false);

            SetProcessingTypeOptions();
        }

        private void SetProcessingTypeOptions()
        {
            List<string> processingTypes = new List<string>();

            foreach (var type in System.Enum.GetValues(typeof(ProcessingType)))
            {
                processingTypes.Add(type.ToString());
            }

            ProcessingTypeDropdown.ClearOptions();
            ProcessingTypeDropdown.AddOptions(processingTypes);
        }

        public void ShowPartSelection(string text)
        {
            _partText.text = text;
            SelectPartButton.GetComponentInChildren<Text>().text = "Select Part";
        }

        public void ShowProcessingTypeOptions()
        {
            ProcessingTypeDropdown.gameObject.SetActive(true);                    

            _processingTypeText.text = "Select processing type";
            _processingTypeText.gameObject.SetActive(true);
        }

        public void ShowToolSelection(string text)
        {
            _toolText.text = text;
            SelectToolButton.GetComponentInChildren<Text>().text = "Select Tool";

            _toolText.gameObject.SetActive(true);
            SelectToolButton.gameObject.SetActive(true);
        }

        public void ShowStatisticsButtons()
        {
            GoToStatisticsButton.gameObject.SetActive(true);
            EditStatisticsButton.gameObject.SetActive(true);
        }
        public void HideStatisticsButtons()
        {
            GoToStatisticsButton.gameObject.SetActive(false);
            EditStatisticsButton.gameObject.SetActive(false);
        }
    }
}
