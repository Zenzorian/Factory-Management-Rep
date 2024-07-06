namespace FactoryManager.Data.Tools
{
    /// <summary>
    /// Другие расходные материалы
    /// </summary>
    public class OtherConsumable : Tool
    {
        public string Description { get; set; }

        public OtherConsumable(string marking, string description, string note)
            : base(marking, note)
        {
            Description = description;
        }
    }
}