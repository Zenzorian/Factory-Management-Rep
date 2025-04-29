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
        public List<Employee> listOfWorkers = new List<Employee>();      

        public List<string> typesOfTools = new List<string>();
        public List<string> typesOfWorkers = new List<string>();
        public List<string> typesOfWorkspace = new List<string>();
        public List<string> typesOfParts = new List<string>();
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