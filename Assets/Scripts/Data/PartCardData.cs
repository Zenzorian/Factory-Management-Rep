namespace Scripts.Data
{
    public class PartCardData
    {
        public Part part;
        public Operation operations;
        public bool itsOperationsAddationButton = false;
        public bool itsStatisticAddationButton = false;

        public PartCardData
        (
            Part part,
            Operation operations,
            bool itsOperationsAddationButton,
            bool itsStatisticAddationButton
        )
        {   
            this.part = part;
            this.operations = operations;
            this.itsOperationsAddationButton = itsOperationsAddationButton;
            this.itsStatisticAddationButton = itsStatisticAddationButton;
        }
    }
}
