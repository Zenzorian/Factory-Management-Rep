using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public class StatisticDataItemAddation : BaseAddition, IItemAddationService
    {        
        private List<StatisticData> _list;
        private InputField _f;
        private InputField _v;

        public StatisticDataItemAddation
        (
             ISaveloadDataService saveloadDataService,
             ItemsAddationViewElements itemsAddationViewElements,
             GlobalUIElements globalUIElements
        ) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
        }

        public void Open(AddationData addationData, Action onAdded)
        {
            Initialize(addationData.menuType, onAdded);

            Clear();

            //_list = list as List<StatisticData>;            
            _addButton.onClick.AddListener(AddToList);

            _f = _inputFieldCreator.Create("F = ", _content);
            _v = _inputFieldCreator.Create("V = ", _content);

            _f.contentType = InputField.ContentType.DecimalNumber;
            _v.contentType = InputField.ContentType.DecimalNumber;
        }
        public void AddToList()
        {
            if (string.IsNullOrEmpty(_f.text) || string.IsNullOrEmpty(_v.text))
                return;
                        
            string fText = _f.text.Replace(',', '.');
            string vText = _v.text.Replace(',', '.');

            double fValue, vValue;
            if (double.TryParse(fText, NumberStyles.Float, CultureInfo.InvariantCulture, out fValue) &&
                double.TryParse(vText, NumberStyles.Float, CultureInfo.InvariantCulture, out vValue))
            {
                var data = new StatisticData
                {
                    F = fValue,
                    V = vValue
                };

                _list.Add(data);               
                _addButton.onClick.RemoveListener(AddToList);

                Added();
            }
            else
            {
                Debug.LogError("Invalid number format in input fields.");
            }
        }


    }
}
