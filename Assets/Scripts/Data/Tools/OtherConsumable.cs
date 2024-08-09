namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Другие расходные материалы
    /// </summary>
    [System.Serializable]
    public class OtherConsumable : Tool
    {
        public string Description;

        public OtherConsumable(string marking, string description, string note, string type)
            : base(marking, note, type)
        {
            Description = description;
        }
    }
}