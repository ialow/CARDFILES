using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class CompanyData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [field: SerializeField] public int Money { get; private set; }
        [field: SerializeField] public List<AssetData> AssetsData { get; private set; }
        [field: SerializeField] public PersonnelData PersonalData { get; private set; }
    }
}
