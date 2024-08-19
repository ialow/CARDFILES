using System.Collections.Generic;
using UnityEngine;
using System;

using Game.Data;

namespace Game.Component
{
    public class DeckCard : MonoBehaviour
    {
        private const int COUNT_CARDS = 8;
        private int numberPrefabs = 0;

        private Company company;

        private List<(GameObject, CardAction)> cardActions = new List<(GameObject, CardAction)> ();

        [SerializeField] private Canvas canvas;

        [Space, SerializeField] private List<GameObject> cardHolder = new List<GameObject>();

        [Space, SerializeField] private List<GameObject> cardActionPrefabs = new List<GameObject>();

        public void Init(Company company)
        {
            this.company = company;
        }

        public void CardsObj(List<CardActionData> data, Action<CardActionData> action, Action<CardActionData> costUsingCardAction)
        {
            DeleteCard();

            for (int i = 0; i < COUNT_CARDS; i++)
            {
                if (numberPrefabs == 0)
                {
                    var cardActionObj = InstantiateCardAction(
                        cardActionPrefabs[numberPrefabs], cardHolder[i].transform, i);

                    var cardAction = cardActionObj.GetComponent<CardAction>();

                    cardAction.Init(data[i], action, costUsingCardAction, canvas,
                        DeleteCardInListAction).UIView();

                    if (company.Money >= data[i].Cost 
                        && company.Personnel.NumberAvailableEmployees >= data[i].NumberEmployees)
                    {
                        cardAction.ActiveCard();
                    }
                    else
                    {
                        cardAction.InactiveCard();
                    }

                    cardActions.Add((cardActionObj, cardAction));

                    numberPrefabs = 1;
                }
                else
                {
                    var cardActionObj = InstantiateCardAction(
                        cardActionPrefabs[numberPrefabs], cardHolder[i].transform, i);

                    var cardAction = cardActionObj.GetComponent<CardAction>();

                    cardAction.Init(data[i], action, costUsingCardAction, canvas,
                        DeleteCardInListAction).UIView();

                    if (company.Money >= data[i].Cost
                        && company.Personnel.NumberAvailableEmployees >= data[i].NumberEmployees)
                    {
                        cardAction.ActiveCard();
                    }
                    else
                    {
                        cardAction.InactiveCard();
                    }

                    cardActions.Add((cardActionObj, cardAction));

                    numberPrefabs = 0;
                }
            }
        }

        private GameObject InstantiateCardAction(GameObject prefab, Transform parentTransform, int numberCard)
        {
            var cardActionObj = Instantiate(cardActionPrefabs[numberPrefabs], parentTransform);
            cardActionObj.name = $"CARD_{numberCard}";

            return cardActionObj;   
        }

        public void IsUsingCard()
        {
            foreach(var cardAction in cardActions)
            {
                var data = cardAction.Item2.Data;

                if (company.Money >= data.Cost
                        && company.Personnel.NumberAvailableEmployees >= data.NumberEmployees)
                {
                    cardAction.Item2.ActiveCard();
                }
                else
                {
                    cardAction.Item2.InactiveCard();
                }
            }
        }

        public void Deinit()
        {
            for (var i = 0; i < cardActions.Count; i++)
            {
                cardActions[i].Item2.InactiveCard();
            }
        }

        private void DeleteCardInListAction(GameObject card, CardAction cardAction)
        {
            cardActions.Remove((card, cardAction));
        }    

        public void DeleteCard()
        {
            for (var i = 0; i < cardHolder.Count; i++)
            {
                if (cardHolder[i].transform.childCount > 0)
                    Destroy(cardHolder[i].transform.GetChild(0).gameObject);
            }

            cardActions.Clear();
        }
    }
}
