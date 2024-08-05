using Game.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Editor;

namespace Game.Component
{
    public class CardAction : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameCard;
        [SerializeField] private TextMeshProUGUI staff;
        [SerializeField] private TextMeshProUGUI cost;

        public Action UsrCardAction { get; private set; }

        public void UIView(CardActionData data)
        {
            nameCard.text = $"{data.Name}";
            staff.text = $"{data.NumberEmployees}";
            cost.text = $"{data.Cost} k";
        }

        public void SetupAction(Action action)
        {
            UsrCardAction = action;
        }
    }
}
