using System;
using UnityEngine;

namespace Game.Data
{
    public class Asset
    {
        public readonly string Name;
        public readonly string Description;

        public int ValueAppraised { get; private set; }
        public int CostSecurity { get; private set; }
        public int IncomeBasic { get; private set; }

        public float PromotionNValue { get; private set; } //
        public float SupportNValue { get; private set; } // 

        public Asset(AssetData data) 
        {
            Name = data.Name;
            Description = data.Description;

            ValueAppraised = data.ValueAppraised;
            CostSecurity = data.CostSecurity;
            IncomeBasic = data.IncomeBasic;

            PromotionNValue = data.PromotionNValue;
            SupportNValue = data.SupportNValue;
        }

        public event Action ChangeParameters;

        public void ChangePromotionNValue(float addedPromotion)
        {
            PromotionNValue = Mathf.Clamp((float)Math.Round(PromotionNValue + addedPromotion, 2), 0f, 1f);

            ChangeParameters?.Invoke();
        }

        public void ChangeSupportNValue(float addedSupport)
        {
            SupportNValue = Mathf.Clamp((float)Math.Round(PromotionNValue + addedSupport, 2), 0f, 1f);

            ChangeParameters?.Invoke();
        }

        public void ChangeBaseParameters(int addedValueAppraised, int addedCostSecurity, int addedIncomeBasic)
        {
            ValueAppraised += addedValueAppraised;
            CostSecurity += addedCostSecurity;
            IncomeBasic += addedIncomeBasic;

            ChangeParameters?.Invoke();
        }
    }
}
