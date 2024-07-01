using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddationController : MonoBehaviour
{
    [SerializeField] private AdditionView _additionView;
    [SerializeField] private GlobalData _globalData;

    public void CloseAddationPanel()
    {
        _additionView.Close();
    }
    public void OpenAdditionPanel(int value)
    {
        Debug.Log((DataType)value);
        _additionView.additionPanel.gameObject.SetActive(true);
        switch ((DataType)value)
        {
            case DataType.Consumable:
                var consumable = new Consumable();
                _additionView.CreateAdditionPanel(consumable);
                break;
            case DataType.Operation:
                var operation = new Operation();
                _additionView.CreateAdditionPanel(operation);
                break;
            case DataType.Part:
                var part = new Part();
                _additionView.CreateAdditionPanel(part);
                break;
            case DataType.Station:
                var station = new Station();
                _additionView.CreateAdditionPanel(station);
                break;
            case DataType.Tool:
                var tool = new Tool();
                _additionView.CreateAdditionPanel(tool);
                break;
            case DataType.Worker:
                var worker = new Worker();
                _additionView.CreateAdditionPanel(worker);                
                break;
            default:
                _additionView.additionPanel.gameObject.SetActive(false);
                break;
        }
    }
}
