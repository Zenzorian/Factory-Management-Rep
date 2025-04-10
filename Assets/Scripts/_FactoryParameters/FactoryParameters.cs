using System;

namespace Scripts.Calculations
{
    public class FactoryParameters
    {
        // Затраты цеха
        public double FactoryStaticCost { get; set; }
        public double FactoryDayCost { get; set; }
        public int FactoryWorkingDays { get; set; }
        public double FactoryFluidCost { get; set; }

        // Заготовка
        public double BarCost { get; set; }
        public double BarLength { get; set; }
        public double BlankLength { get; set; }

        // Рабочие и инструменты
        public double WorkerCost { get; set; }
        public double ToolCost { get; set; }

        // Прочие параметры
        public double ProductSaleCost { get; set; }
        public double MachineDailyWorkTime { get; set; }
        public int MachineWorkingDays { get; set; }
        public double WorkerShiftHours { get; set; }
    }
    
}
