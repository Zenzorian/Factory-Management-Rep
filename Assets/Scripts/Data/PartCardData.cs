namespace Scripts.Data
{
    public class PartCardData
    {
        public Part part;
        public Operation operation;
        public Statistic statistic;       

        public PartCardData
        (
            Part part,
            Operation operation,
            Statistic statistic           
        )
        {   
            this.part = part;
            this.operation = operation;
            this.statistic = statistic;           
        }
    }
}
