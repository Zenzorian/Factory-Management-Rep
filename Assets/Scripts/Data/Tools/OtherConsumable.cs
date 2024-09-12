namespace FactoryManager.Data.Tools
{
    [System.Serializable]
    public class OtherConsumable : Tool
    {
        public string Description;

        public OtherConsumable(int id, string name, string description, string note, string type)
            : base(id,name, note, type)
        {
            Description = description;
        }
    }
}