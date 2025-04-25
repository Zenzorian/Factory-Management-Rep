using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.UI.Markers;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticDataView
    {
        public Button SelectPartButton { get; private set; }       

        public Button GoToStatisticsButton { get; private set; }
        public Button EditStatisticsButton { get; private set; }

        private readonly ITableProcessorService _tableProcessorService;

        private readonly Text _partText;       

        private GameObject _statisticPanel;
        private Transform _statisticViewContainer;
        public ChoiceOfStatisticDataView(StatisticViewElements elements, ITableProcessorService tableProcessorService)
        {
            _partText = elements.partText;

            _tableProcessorService = tableProcessorService;
            SelectPartButton = elements.selectPartButton;  

            GoToStatisticsButton = elements.goToStatisticsButton;
            EditStatisticsButton = elements.editStatisticsButton;
                
            _statisticPanel = elements.gameObject;
            _statisticViewContainer = elements.statisticViewContainer;  


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
                       
            GoToStatisticsButton.gameObject.SetActive(false);
        }       

        public void ShowPartSelection(string text)
        {
            _partText.text = text;
            SelectPartButton.GetComponentInChildren<Text>().text = "Select Part";
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

        public void ShowOperations(Part part, StatisticTableActions statisticTableActions)
        {
            _tableProcessorService.CreateColumnBasedTable(part, statisticTableActions, _statisticViewContainer);
        }
        public void OnEditMode()
        {
            _tableProcessorService.SetEditMode();
        }
    }
}
