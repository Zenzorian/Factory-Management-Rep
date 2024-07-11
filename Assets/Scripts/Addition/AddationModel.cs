using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryManager
{
    public class AddationModel : MonoBehaviour
    {
        [SerializeField] private GlobalData _globalData;

        //private void Awake()
        //{
        //    _globalData.listOfWorkers = WorkersGenerator.GenerateWorkers(50);
        //}
        public void AddToList(TableItem tableItem)
        {
            System.Type type = tableItem.GetType();
            switch (type.Name)
            {
                case "Consumable":
                    _globalData.listOfConsumable.Add((Consumable)tableItem);
                    break;               
                case "Part":
                    _globalData.listOfParts.Add((Part)tableItem);
                    break;
                case "Station":
                    _globalData.listOfWorkstations.Add((Workstation)tableItem);
                    break;
                case "Tool":
                    _globalData.listOfTools.Add((Tool)tableItem);
                    break;
                case "Worker":
                    _globalData.listOfWorkers.Add((Worker)tableItem);
                    break;
                default:
                    break;
            }
        }
    }
}