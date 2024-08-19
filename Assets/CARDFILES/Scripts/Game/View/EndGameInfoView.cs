using Game.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class EndGameInfoView : MonoBehaviour
    {
        private string[] descriptionResult = new string[] { "общая стоимость активов -", ", это на", "больше изначального капитала." };

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

            resultPart.text = $"{descriptionResult[0]} {216} {descriptionResult[1]} {34} {descriptionResult[2]}";
        }
    }
}
