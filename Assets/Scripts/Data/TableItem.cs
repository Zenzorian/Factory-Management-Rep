namespace Scripts.Data
{
    [System.Serializable]
    public class TableItem
    {
        public TableItem(int id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
        public int Id;
        public string Name;
        public string Type;
    }
}