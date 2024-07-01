
namespace FactoryManager.Data
{
    /// <summary>
    /// Работник
    /// </summary>
    [System.Serializable]
    public class Worker : TableItem
    {
        public string id;

        public string firstName;
        public string lastName;
        public string position;

        public string weeklyNorm;
        public string overtimeAllowed;

        public string hourlyWage;
        public string overtimeSurcharge;
        public string nightShiftSurcharge;
        public Worker()
        {

        }
        public Worker(string id, string firstName, string lastName, string position,
                      string weeklyNorm = "40", string overtimeAllowed = "0",
                      string hourlyWage = "0", string overtimeSurcharge = "0",
                      string nightShiftSurcharge = "0")
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.weeklyNorm = weeklyNorm;
            this.overtimeAllowed = overtimeAllowed;
            this.hourlyWage = hourlyWage;
            this.overtimeSurcharge = overtimeSurcharge;
            this.nightShiftSurcharge = nightShiftSurcharge;
        }
    }
}