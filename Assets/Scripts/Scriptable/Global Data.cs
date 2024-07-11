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
        public List<Workstation> listOfWorkstations = new List<Workstation>();
        public List<Tool> listOfTools = new List<Tool>();
        public List<Worker> listOfWorkers = new List<Worker>();

        public static List<string> typesOfTools = new List<string>
        {
            "LatheCNC",
            "MillingCNC",
            "Cutting",
            "Grooving",
            "ThreadingMachines",
            "Drills",
            "MillingCutters",
            "Taps",
            "Other"
        };
        public static List<string> typesOfWorkers = new List<string>
        {
            "Manager",
            "Engineer",
            "Technician",
            "Welder",
            "Assembler",
            "QualityControl",
            "Maintenance",
            "ForkliftOperator",
            "Painter",
            "Packer"
        };
        public static List<string> typesOfWorkspaces = new List<string>
        {
            "AssemblyLine",
            "CNC",
            "Painting",
            "QualityControl",
            "Packaging",
            "Maintenance",
            "Storage",
            "Welding",
            "Inspection",
            "ResearchAndDevelopment"
        };
        public static List<string> typesOfParts = new List<string>
        {
            "Shaft",
            "Gear",
            "Housing",
            "Bracket",
            "Bushing",
            "Cylinder",
            "Pulley",
            "Plate",
            "Spacer",
            "Ring"
        };
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