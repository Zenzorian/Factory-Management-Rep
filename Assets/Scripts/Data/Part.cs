/// <summary>
/// Желаемая деталь
/// </summary>
[System.Serializable]
public class Part : TableItem
{
    public Operation[] operations;
    public string tool;
    public string statistics;
    public Part()
    { 
    
    }
    public Part(Operation[] operations, string tool, string statistics)
    {
        this.operations = operations;
        this.tool = tool;
        this.statistics = statistics;
    }
}