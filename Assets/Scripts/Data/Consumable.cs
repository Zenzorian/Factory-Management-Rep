namespace Scripts.Data
{
    /// <summary>
    /// Расходный материал
    /// </summary>
    public class Consumable : TableItem
    {
        public Consumable(int id, string name, string type) : base(id, name, type)
        {
        }
    }
}