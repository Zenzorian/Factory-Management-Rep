namespace FactoryManager.Data
{
    [System.Serializable]
    public abstract class TableItem
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