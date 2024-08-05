using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class PersonnelData
    {
        [field: SerializeField] public int NumberEmployees { get; private set; }
        [field: SerializeField] public int SalaryTotalForPart { get; private set; }
    }
}
