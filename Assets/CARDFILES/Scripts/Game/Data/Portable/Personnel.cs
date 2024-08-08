using System;
using System.Diagnostics;

namespace Game.Data
{
    public class Personnel
    {
        public int NumberAvailableEmployees { get; private set; }
        public int NumberEmployedEmployees { get; private set; } = 0;

        public float SatisfactionNValue { get; private set; } = 0.5f;
        public int SalaryTotalForPart { get; private set; }

        public event Action DistributionEmployeesEvent;

        public Personnel(PersonnelData data)
        {
            NumberAvailableEmployees = data.NumberEmployees;
            SalaryTotalForPart = data.SalaryTotalForPart;   
        }

        public void MakeTask(int countRequiredEmployees)
        {
            NumberAvailableEmployees -= countRequiredEmployees;
            NumberEmployedEmployees += countRequiredEmployees;
            
            DistributionEmployeesEvent?.Invoke();
        }

        public void CompleteTask(int countReleasedEmployees)
        {
            NumberEmployedEmployees -= countReleasedEmployees;
            NumberAvailableEmployees += countReleasedEmployees;

            DistributionEmployeesEvent?.Invoke();
        }
    }
}
