using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Scriptable object/HistoryData")]
    public class HistorySO : ScriptableObject
    {
        [field : SerializeField] public CompanyData Company { get; private set; }
        [field : SerializeField] public List<EventData> Events { get; private set; }


        //[field: SerializeField] public float LoanRate { get; private set; }
    }
}
