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

        _globalData.listOfParts = GenerateParts(15);
    }
    public List<Part> GenerateParts(int count)
    {
        List<Part> parts = new List<Part>();

        for (int i = 0; i < count; i++)
        {
            var part = new Part(
                id:i,
                type : _globalData.typesOfParts[Random.Range(1, 6)],
                name : $"Part {_globalData.typesOfParts[Random.Range(1, 6)]} {i + 1}"           
            );
            part.Statistic = GenerateStatisticsList(_globalData.listOfTools[0],ProcessingType.Finishing,10,3);

            parts.Add(part);
        }

        return parts;
    }
    public static List<Statistic> GenerateStatisticsList(Tool tool, ProcessingType processingType, int numberOfDataPointsPerStatistic, int numberOfStatistics)
    {
        List<Statistic> statisticsList = new List<Statistic>();

        System.Random random = new System.Random();

        for (int i = 0; i < numberOfStatistics; i++)
        {            
            Statistic statistics = new Statistic(tool, processingType);

            for (int j = 0; j < numberOfDataPointsPerStatistic; j++)
            {
                List<int> ints = new List<int>();
                for (int k = 0; k < 5; k++)
                {
                    ints.Add(random.Next(50, 250));
                }
                StatisticData data = new StatisticData
                {
                    F = System.Math.Round(Random.Range(0f,5f), 3), 
                    V = System.Math.Round(Random.Range(0f,5f), 3), 
                    PartCounter = ints
                };
               

                statistics.Data.Add(data);
            }

            statisticsList.Add(statistics);
        }

        return statisticsList;
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
                name:$"{firstNames[Random.Range(0,firstNames.Length)]} {lastNames[Random.Range(0,lastNames.Length)]}",
                _globalData.typesOfWorkers[UnityEngine.Random.Range(0, 10)]
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
            var type = _globalData.typesOfWorkstation[Random.Range(1, 9)];
            Tool[] tools = GenerateRandomTools(Random.Range(1, 3)).ToArray();
            int maxWorkers = Random.Range(1, 20);
            int reservedWorkers = Random.Range(0, maxWorkers);

            workstations.Add(new Workstation(
                i,
                name : $"Workstation {_globalData.typesOfWorkstation[Random.Range(1, 6)]} {i + 1}",
                type,
                tools,
                maxWorkers,
                reservedWorkers
                ));
        }

        return workstations;
    }
    CNCMillingTool CreateTurningRoughingTool()
    {
        var tool = new CNCMillingTool(
            id:Random.Range(0,100),
            name: GenerateRandomString(),
            note: "Random note for Turning Roughing Tool",
            type: "MillingCNC",
            new ManufacturersRecomendedParametrs(fMin: UnityEngine.Random.Range(0.1f, 1f),
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
            id:Random.Range(0,100),
            name: GenerateRandomString(),
            note: "Random note for Turning Roughing Tool",
            type: "Grooving",
            new ManufacturersRecomendedParametrs(fMin: UnityEngine.Random.Range(0.1f, 1f),
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
            id:Random.Range(0,100),
            name: GenerateRandomString(),
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
            id:Random.Range(0,100),
            name: GenerateRandomString(),
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
             id:Random.Range(0,100),
            name: GenerateRandomString(),
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

