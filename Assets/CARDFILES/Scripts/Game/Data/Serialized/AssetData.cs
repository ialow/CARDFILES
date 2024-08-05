using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class AssetData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [field: SerializeField] public int ValueAppraised { get; private set; }
        [field: SerializeField] public int CostSecurity { get; private set; }
        [field: SerializeField] public int IncomeBasic { get; private set; }

        [field: SerializeField, Range(0f, 1f)] public float PromotionNValue { get; private set; } = 0.5f;
        [field: SerializeField, Range(0f, 1f)] public float SupportNValue { get; private set; } = 0.5f;
    }
}
