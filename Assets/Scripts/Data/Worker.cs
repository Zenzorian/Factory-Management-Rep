namespace Scripts.Data
{    
    [System.Serializable]
    public class Worker : TableItem
    {        
        public float WeeklyNorm;
        public float OvertimeAllowed;

        public float HourlyWage;
        public float OvertimeSurcharge;
        public float NightShiftSurcharge;
        public Worker(int id, string name, string type):base(id,name,type)
        {
            Id = id;
            Name = name;            
            Type = type;
        }
        public Worker(int id, string name, string type,
                      float weeklyNorm = 40, float overtimeAllowed = 0,
                      float hourlyWage = 0, float overtimeSurcharge = 0,
                      float nightShiftSurcharge = 0):base(id,name,type)
        {
            Id = id;            
            Type = type;
            WeeklyNorm = weeklyNorm;
            OvertimeAllowed = overtimeAllowed;
            HourlyWage = hourlyWage;
            OvertimeSurcharge = overtimeSurcharge;
            NightShiftSurcharge = nightShiftSurcharge;
        }
    }   
}