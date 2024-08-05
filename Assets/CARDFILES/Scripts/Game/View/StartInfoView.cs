using Game.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class StartInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private List<TextMeshProUGUI> assets = new List<TextMeshProUGUI>();

        public void UIView(Company data)
        {
            description.text = $"{data.Description}";

            var countAssets = assets.Count;
            for (var i = 0; i < countAssets; i++)
            {
                assets[i].text = $"{data.Assets[i].Name}";
            }
        }
    }
}
