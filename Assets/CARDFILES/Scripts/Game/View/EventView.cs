using Game.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameEvent;
        [SerializeField] private TextMeshProUGUI description;

        public void UIView(EventData data)
        {
            nameEvent.text = $"{data.Name}";
            description.text = $"{data.Description}";
        }
    }
}
