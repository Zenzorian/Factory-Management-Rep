using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsInputManager : MonoBehaviour
{
    [SerializeField] private ButtonCreator _buttonCreator;

    [SerializeField] private Text _fText;
    [SerializeField] private Text _vText;
    [SerializeField] private Button _AddButton;
    [SerializeField] private InputField _AddInput;

    [SerializeField] private Transform _content;

    private StatisticData _currentStatisticData = new StatisticData();

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
        if (string.IsNullOrEmpty(_AddInput.text))return;

        var data = int.Parse(_AddInput.text);       
        _currentStatisticData.PartCounter.Add(data);
        _AddInput.text = "";

        ShowPanel(_currentStatisticData);

    }
    public void Clear()
    {
        foreach (Transform item in _content)
        {
            Destroy(item.gameObject);
        }        
    }
}
