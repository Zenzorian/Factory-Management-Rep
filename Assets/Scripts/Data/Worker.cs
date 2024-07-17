
namespace FactoryManager.Data
{
    /// <summary>
    /// Работник
    /// </summary>
    [System.Serializable]
    public class Worker : TableItem
    {
        public int Id;

        public string FirstName;
        public string LastName;      

        public float WeeklyNorm;
        public float OvertimeAllowed;

        public float HourlyWage;
        public float OvertimeSurcharge;
        public float NightShiftSurcharge;
        
        public Worker(int id, string firstName, string lastName, string type,
                      float weeklyNorm = 40, float overtimeAllowed = 0,
                      float hourlyWage = 0, float overtimeSurcharge = 0,
                      float nightShiftSurcharge = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Type = type;
            WeeklyNorm = weeklyNorm;
            OvertimeAllowed = overtimeAllowed;
            HourlyWage = hourlyWage;
            OvertimeSurcharge = overtimeSurcharge;
            NightShiftSurcharge = nightShiftSurcharge;
        }
    }   
}