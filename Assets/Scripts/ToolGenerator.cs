using UnityEngine;
namespace FactoryManager.Data.Tools
{
    public class ToolGenerator : MonoBehaviour
    {
        public ToolRepository toolRepository = new ToolRepository();

        void Start()
        {
            GenerateRandomTools(10); // Генерируем 10 случайных инструментов                       
        }

        void GenerateRandomTools(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int toolType = UnityEngine.Random.Range(0, 4);
                Tool newTool = null;

                switch (toolType)
                {
                    case 0:
                        //newTool = CreateTurningRoughingTool();
                        break;
                    case 1:
                       // newTool = CreateGroovingTool();
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
                    toolRepository.AddTool(newTool);
                }
            }
        }

        CNCMillingTool CreateTurningRoughingTool()
        {
            var tool = new CNCMillingTool(
                marking: GenerateRandomString(),
                fMin: UnityEngine.Random.Range(0.1f, 1f),
                fMax: UnityEngine.Random.Range(1f, 3f),
                vMin: UnityEngine.Random.Range(100f, 200f),
                vMax: UnityEngine.Random.Range(200f, 400f),
                cost: (decimal)UnityEngine.Random.Range(10f, 100f),
                note: "Random note for Turning Roughing Tool",
                type: MachineTool.MillingCNC
                );

            return tool;
        }

        GroovingTool CreateGroovingTool()
        {
            var tool = new GroovingTool(
                marking: GenerateRandomString(),
                fMin: UnityEngine.Random.Range(0.1f, 1f),
                fMax: UnityEngine.Random.Range(1f, 3f),
                vMin: UnityEngine.Random.Range(100f, 200f),
                vMax: UnityEngine.Random.Range(200f, 400f),
                width: UnityEngine.Random.Range(1f, 100f),
                cost: (decimal)UnityEngine.Random.Range(10f, 100f),
                note: "Random note for Turning Roughing Tool",
                type: MachineTool.Grooving
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
                type: MachineTool.ThreadingMachines
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
                type: MachineTool.Taps
                );
            return tool;
        }

        OtherConsumable CreateOtherConsumable()
        {
            var tool = new OtherConsumable(
                marking: GenerateRandomString(),
                description: "Random description",
                note: "Random note for Other Consumable",
                type: MachineTool.Other
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
}