using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
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