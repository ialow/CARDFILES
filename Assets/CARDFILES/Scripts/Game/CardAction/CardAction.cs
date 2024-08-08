using System;
using TMPro;
using UnityEngine;

using Game.Data;
using UnityEngine.UI;

namespace Game.Component
{
    public class CardAction : MonoBehaviour
    {
        private Action<CardActionData> action;
        private Action<CardActionData> usageCosts;
        private Action<GameObject, CardAction> removeCard;

        [SerializeField] private TextMeshProUGUI nameCard;
        [SerializeField] private TextMeshProUGUI staff;
        [SerializeField] private TextMeshProUGUI cost;

        [SerializeField] private GameObject inactiveCard;
        [SerializeField] private DraggableObject draggable;

        public CardActionData Data { get; private set; }

        public CardAction Init(CardActionData data, 
            Action<CardActionData> action, Action<CardActionData> usageCosts, Canvas canvas,
            Action<GameObject, CardAction> removeCard)
        {
            Data = data;

            this.action = action;
            this.usageCosts = usageCosts;
            this.removeCard = removeCard;

            draggable.Init(canvas);

            return this;
        }

        public void UIView()
        {
            nameCard.text = $"{Data.Name}";
            staff.text = $"{Data.NumberEmployees}";
            cost.text = $"{Data.Cost} k";
        }

        public void UseCard()
        {
            removeCard?.Invoke(gameObject, this);
            usageCosts?.Invoke(Data);
            action?.Invoke(Data);
        }

        public void ActiveCard()
        {
            inactiveCard.SetActive(false);
            draggable.Active();
        }

        public void InactiveCard()
        {
            draggable.Inactive();
            inactiveCard.SetActive(true);
        }
    }
}
