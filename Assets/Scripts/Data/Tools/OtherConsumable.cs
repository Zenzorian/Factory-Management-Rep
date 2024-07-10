namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Другие расходные материалы
    /// </summary>
    [System.Serializable]
    public class OtherConsumable : Tool
    {
        public string Description { get; set; }

        public OtherConsumable(string marking, string description, string note, MachineTool type)
            : base(marking, note,type)
        {
            Description = description;
        }
    }
}