using Game.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class PartInfoView : MonoBehaviour
    {
        private string[] descriptionResult = new string[] { "доход за ход - ", ", расход за ход - " };

        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private List<TextMeshProUGUI> assetsName = new List<TextMeshProUGUI>();
        [SerializeField] private TextMeshProUGUI resultPart;

        public void UIView(Company data)
        {
            //description.text = $"{data.Description}"; // добавить фразы

            var countAssets = assetsName.Count;
            for (var i = 0; i < countAssets; i++)
            {
                assetsName[i].text = $"{data.Assets[i].Name}";
            }

            resultPart.text = $"{descriptionResult[0]} {data.IncomeForPart} {descriptionResult[1]} {data.ExpensesForPart}";
        }
    }
}
