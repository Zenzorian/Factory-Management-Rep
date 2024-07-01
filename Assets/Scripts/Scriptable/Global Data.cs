using FactoryManager.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryManager
{
    [System.Serializable, CreateAssetMenu]
    public class GlobalData : ScriptableObject
    {
        public List<Consumable> listOfConsumable = new List<Consumable>();
        public List<Operation> listOfOperation = new List<Operation>();
        public List<Part> listOfPart = new List<Part>();
        public List<Station> listOfStation = new List<Station>();
        public List<Tool> listOfTool = new List<Tool>();
        public List<Worker> listOfWorkers = new List<Worker>();
    }
    public enum DataType
    {
        Consumable,
        Operation,
        Part,
        Station,
        Tool,
        Worker
    }
}