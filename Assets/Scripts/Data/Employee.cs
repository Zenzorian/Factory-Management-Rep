using System;
using System.Collections.Generic;

namespace Scripts.Data
{    
    [System.Serializable]
    public class Employee : TableItem
    {        
        public float WeeklyNorm;
        public float OvertimeAllowed;

        public float HourlyWage;
        public float OvertimeSurcharge;
        public float NightShiftSurcharge;
        
        public  List<EmployeeShift> employeeShifts;
        public Employee(int id, string name, string type):base(id,name,type)
        {
            Id = id;
            Name = name;            
            Type = type;
        }
        public Employee(int id, string name, string type,
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

    public class EmployeeShift
    {
        public DateTime date;
        public List<EmployeeWork> history;
    }
    
    public class EmployeeWork
    {
        public EmployeeWork(Workspace workspace, Tool tool, float time)
        {
            this.workspace = workspace;
            this.tool = tool;
            this.time = time;
        }
        public Workspace workspace;
        public Tool tool;
        public float time;
    }
}