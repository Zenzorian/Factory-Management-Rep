
namespace FactoryManager.Data
{
    /// <summary>
    /// Работник
    /// </summary>
    [System.Serializable]
    public class Worker : TableItem
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string  Type { get; set; }

        public float WeeklyNorm { get; set; }
        public float OvertimeAllowed { get; set; }

        public float HourlyWage { get; set; }
        public float OvertimeSurcharge { get; set; }
        public float NightShiftSurcharge { get; set; }
        public Worker()
        {

        }
        public Worker(int id, string firstName, string lastName, string position,
                      float weeklyNorm = 40, float overtimeAllowed = 0,
                      float hourlyWage = 0, float overtimeSurcharge = 0,
                      float nightShiftSurcharge = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Type = position;
            WeeklyNorm = weeklyNorm;
            OvertimeAllowed = overtimeAllowed;
            HourlyWage = hourlyWage;
            OvertimeSurcharge = overtimeSurcharge;
            NightShiftSurcharge = nightShiftSurcharge;
        }
    }   
}