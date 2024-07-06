
namespace FactoryManager.Data
{
    /// <summary>
    /// Работник
    /// </summary>
    [System.Serializable]
    public class Worker : TableItem
    {
        public int id;

        public string firstName;
        public string lastName;
        public FactoryWorker position;

        public float weeklyNorm;
        public float overtimeAllowed;

        public float hourlyWage;
        public float overtimeSurcharge;
        public float nightShiftSurcharge;
        public Worker()
        {

        }
        public Worker(int id, string firstName, string lastName, FactoryWorker position,
                      float weeklyNorm = 40, float overtimeAllowed = 0,
                      float hourlyWage = 0, float overtimeSurcharge = 0,
                      float nightShiftSurcharge = 0)
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

    public enum FactoryWorker
    {
        Manager,          // Управляющий
        Engineer,         // Инженер
        Technician,       // Техник
        Welder,           // Сварщик
        Assembler,        // Сборщик
        QualityControl,   // Контроль качества
        Maintenance,      // Обслуживание
        ForkliftOperator, // Водитель погрузчика
        Painter,          // Маляр
        Packer            // Упаковщик
    }
}