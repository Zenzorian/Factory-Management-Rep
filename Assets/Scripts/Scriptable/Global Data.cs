using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryManager
{
    [System.Serializable, CreateAssetMenu]
    public class GlobalData : ScriptableObject
    {
        public List<Consumable> listOfConsumable = new List<Consumable>();      
        public List<Part> listOfParts = new List<Part>();
        public List<Workspace> listOfWorkspaces = new List<Workspace>();
        public List<Tool> listOfTools = new List<Tool>();
        public List<Worker> listOfWorkers = new List<Worker>();      

        public List<string> typesOfTools;
        public List<string> typesOfWorkers;
        public List<string> typesOfWorkspace;
        public List<string> typesOfParts;
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