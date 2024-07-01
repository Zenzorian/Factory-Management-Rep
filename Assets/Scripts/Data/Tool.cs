namespace FactoryManager.Data
{
    /// <summary>
    /// Инструмент
    /// </summary>
    [System.Serializable]
    public class Tool : TableItem
    {
        public string type;
        public string characteristics;
        public int resource;

        public Tool()
        {

        }
        public Tool(string type, string characteristics, int resource)
        {
            this.type = type;
            this.characteristics = characteristics;
            this.resource = resource;
        }
    }
}