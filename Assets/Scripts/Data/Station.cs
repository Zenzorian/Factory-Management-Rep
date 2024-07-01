/// <summary>
/// Рабочий участок
/// </summary>
[System.Serializable]
public class Station : TableItem
{
    public string type;
    public int maxWorkers;
    public int reservedWorkers;
    public Station()
    { }
    public Station(string type, int maxWorkers, int reservedWorkers)
    {
        this.type = type;
        this.maxWorkers = maxWorkers;
        this.reservedWorkers = reservedWorkers;
    }
}
