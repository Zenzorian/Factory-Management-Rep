using System;
using System.Collections.Generic;
using Scripts.Data;

public static class ProductionCalculations
    {
      
        public static double CalculateFactoryMaintenanceCost
        (
            double factoryStaticCost,
            double factoryFluidCost,
            double factoryDayCost,
            int factoryWorkingDays)
        {
            return factoryStaticCost + factoryFluidCost + factoryDayCost * factoryWorkingDays;
        }
       
        public static double CalculateBlankCost(double barCost, double barLength, double blankLength)
        {
            int nCutPrtInt = (int)Math.Floor(barLength / blankLength);
            if(nCutPrtInt == 0 || blankLength > barLength)
                throw new InvalidOperationException("Невозможно получить заготовки с заданными размерами.");
            return barCost / nCutPrtInt;
        }

        /// <summary>
        /// Распределение зарплаты работника по станкам.
        /// </summary>
        public static double CalculateMachineSalaryAllocation(EmployeeShift employeeShift, double employeeCost, Tool tool)
        {
            if (employeeShift == null)
                throw new ArgumentException(nameof(employeeShift) + " can't be null.");
            
            if (employeeShift.history.Count == 0 ||employeeShift.history == null)
                throw new ArgumentException(nameof(employeeShift.history) + " can't be null or empty.");

            double employeeTotalWorkTime = GetSum(employeeShift.history);

            if (employeeTotalWorkTime <= 0)
                throw new InvalidOperationException("Общее время работы станков должно быть больше нуля.");

            var currentToolTime = employeeShift.history.Find(e => e.tool == tool);
            
            return employeeTotalWorkTime / currentToolTime.time  * employeeCost;
        }

        private static double GetSum(List<EmployeeWork> employeeShiftHistory)
        {
            double sum = 0;
            foreach (EmployeeWork employeeWork in employeeShiftHistory)
            {
                sum += employeeWork.time;
            }
            return sum;
        }
    }

