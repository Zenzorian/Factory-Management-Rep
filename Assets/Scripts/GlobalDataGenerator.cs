using FactoryManager;
using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataGenerator : MonoBehaviour
{
    [SerializeField]private GlobalData _globalData;

    private void Start()
    {
        _globalData.listOfWorkers = GenerateWorkers(50);
        _globalData.listOfTools = GenerateRandomTools(50);
        _globalData.listOfWorkstations = GenerateWorkstations(15);
        _globalData.listOfParts = GenerateParts(50);

    }
    public List<Part> GenerateParts(int count)
    {
        List<Part> parts = new List<Part>();

        for (int i = 0; i < count; i++)
        {
            string name = $"Part {i + 1}";
            Operation[] operations = GenerateRandomOperations(Random.Range(1, 5)).ToArray();
            string statistics = $"Statistics for part {i + 1}";
            string partType = GlobalData.typesOfParts[Random.Range(1, 6)];

            parts.Add(new Part(name, partType, operations, statistics));
        }

        return parts;
    }

    private List<Operation> GenerateRandomOperations(int count)
    {
        List<Operation> operations = new List<Operation>();

        for (int i = 0; i < count; i++)
        {
            string name = $"Operation {i + 1}";
            Tool[] tools = GenerateRandomTools(Random.Range(1, 3)).ToArray();

            operations.Add(new Operation(name, tools));
        }

        return operations;
    }
    public List<Worker> GenerateWorkers(int numberOfWorkers)
    {
        List<Worker> workers = new List<Worker>();       

        string[] firstNames = { "John", "Peter", "Sergey", "Alex", "Dmitry", "Michael", "George", "Nick", "Alexander", "Max" };
        string[] lastNames = { "Johnson", "Peterson", "Smith", "Alexeev", "Dmitriev", "Mikhailov", "Georgiev", "Nikitsin", "Alexandrov", "Maxwell" };

        for (int i = 1; i <= numberOfWorkers; i++)
        {
            workers.Add(new Worker(
                i,
                firstNames[Random.Range(0,firstNames.Length)],
                lastNames[Random.Range(0,lastNames.Length)],
                GlobalData.typesOfWorkers[UnityEngine.Random.Range(0, 10)]
            ));
        }

        return workers;
    }
    private List<Tool> GenerateRandomTools(int count)
    {
        var toolList = new List<Tool>();

        for (int i = 0; i < count; i++)
        {
            int toolType = UnityEngine.Random.Range(0, 4);
            Tool newTool = null;

            switch (toolType)
            {
                case 0:
                    newTool = CreateTurningRoughingTool();
                    break;
                case 1:
                    newTool = CreateGroovingTool();
                    break;
                case 2:
                    newTool = CreateThreadingTool();
                    break;
                case 3:
                    newTool = CreateTap();
                    break;
                default:
                    newTool = CreateOtherConsumable();
                    break;
            }

            if (newTool != null)
            {
                toolList.Add(newTool);
            }            
        }
        return toolList;
    }
    public List<Workstation> GenerateWorkstations(int count)
    {
        List<Workstation> workstations = new List<Workstation>();

        for (int i = 0; i < count; i++)
        {
            var type = GlobalData.typesOfWorkspaces[Random.Range(1, 9)];
            Tool[] tools = GenerateRandomTools(Random.Range(1, 3)).ToArray();
            int maxWorkers = Random.Range(1, 20);
            int reservedWorkers = Random.Range(0, maxWorkers);

            workstations.Add(new Workstation(type, tools, maxWorkers, reservedWorkers));
        }

        return workstations;
    }
    CNCMillingTool CreateTurningRoughingTool()
    {
        var tool = new CNCMillingTool(
            marking: GenerateRandomString(),
            note: "Random note for Turning Roughing Tool",
            type: "MillingCNC",
            new ToolStatistic(fMin: UnityEngine.Random.Range(0.1f, 1f),
            fMax: UnityEngine.Random.Range(1f, 3f),
            vMin: UnityEngine.Random.Range(100f, 200f),
            vMax: UnityEngine.Random.Range(200f, 400f),
            partCount: UnityEngine.Random.Range(1, 100)),           
            cost: (decimal)UnityEngine.Random.Range(10f, 100f)
            
            );

        return tool;
    }

    GroovingTool CreateGroovingTool()
    {
        var tool = new GroovingTool(
            marking: GenerateRandomString(),
            note: "Random note for Turning Roughing Tool",
            type: "Grooving",
            new ToolStatistic(fMin: UnityEngine.Random.Range(0.1f, 1f),
            fMax: UnityEngine.Random.Range(1f, 3f),
            vMin: UnityEngine.Random.Range(100f, 200f),
            vMax: UnityEngine.Random.Range(200f, 400f),
            partCount: UnityEngine.Random.Range(1, 100)),
            width: UnityEngine.Random.Range(1f, 100f),
            cost: (decimal)UnityEngine.Random.Range(10f, 100f)            
            );

        return tool;
    }

    ThreadingTool CreateThreadingTool()
    {
        var tool = new ThreadingTool(
            marking: GenerateRandomString(),
            location: (ThreadingTool.LocationType)UnityEngine.Random.Range(0, 2),
            measurement: (MeasurementSystem)UnityEngine.Random.Range(0, 2),
            vMin: UnityEngine.Random.Range(50f, 100f),
            vMax: UnityEngine.Random.Range(100f, 200f),
            pitch: UnityEngine.Random.Range(0.5f, 2f),
            note: "Random note for Threading Tool",
            type: "ThreadingMachines"
            );

        return tool;
    }


    TapTool CreateTap()
    {
        var tool = new TapTool(
             marking: GenerateRandomString(),
            measurement: (MeasurementSystem)UnityEngine.Random.Range(0, 2),
            pitch: UnityEngine.Random.Range(0.5f, 2f),
            vMin: UnityEngine.Random.Range(50f, 100f),
            vMax: UnityEngine.Random.Range(100f, 200f),
            note: "Random note for Tap",
            type: "Taps"
            );
        return tool;
    }

    OtherConsumable CreateOtherConsumable()
    {
        var tool = new OtherConsumable(
            marking: GenerateRandomString(),
            description: "Random description",
            note: "Random note for Other Consumable",
            type: "Other"
            );

        return tool;
    }

    string GenerateRandomString()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new System.Random();
        char[] stringChars = new char[8];
        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        return new string(stringChars);
    }
}

