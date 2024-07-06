using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public char SelectedSymbol { get; private set; }
    [SerializeField] private Dropdown _dropdown;

    private Dictionary<string, char> _currencySymbols = new Dictionary<string, char>()
    {
        { "USD", '$' },  // Доллар США
        { "EUR", '€' },  // Евро
        { "JPY", '¥' },  // Японская иена
        { "GBP", '£' },  // Фунт стерлингов Великобритании
        { "CNY", '¥' }   // Китайский юань
    };
        
    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnCurrencySelected);
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();

        foreach (var currency in _currencySymbols)
        {
            Dropdown.OptionData option = new Dropdown.OptionData($"{currency.Key} - {currency.Value}");
            dropdownOptions.Add(option);
        }

        _dropdown.options = dropdownOptions;
    }

    public void OnCurrencySelected(int index)
    {
        string selectedCurrency = _dropdown.options[index].text.Split('-')[0].Trim();
        SelectedSymbol = _currencySymbols[selectedCurrency];
        Debug.Log($"Выбранная валюта: {selectedCurrency}, Символ: {SelectedSymbol}");
    }
}
