using System.Collections.Generic;
using UnityEngine;

using Game.Component;
using Game.Data;
using System;

namespace Game.View
{
    public class DeckCardView : MonoBehaviour
    {
        private const int COUNT_CARDS = 8;
        private int numberPrefabs = 0;
        private int numberCard = 0;

        [SerializeField] private Canvas canvas;

        [Space, SerializeField] private List<GameObject> cardHolder = new List<GameObject>();

        [Space, SerializeField] private List<GameObject> cardActionPrefabs = new List<GameObject>();

        public void CardsObj(List<CardActionData> data, Action<string> action)
        {
            for (int i = 0; i < COUNT_CARDS; i++)
            {
                if (numberPrefabs == 0)
                {
                    var cardActionObj = Instantiate(cardActionPrefabs[numberPrefabs], cardHolder[i].transform);
                    cardActionObj.name = $"CARD_{numberCard}";

                    cardActionObj.GetComponent<DraggableObject>().Init(canvas);

                    var cardAction = cardActionObj.GetComponent<CardAction>();
                    cardAction.SetupAction(() => action(cardActionObj.name));
                    cardAction.UIView(data[i]);

                    numberCard++;
                    numberPrefabs = 1;
                }
                else
                {
                    var cardActionObj = Instantiate(cardActionPrefabs[numberPrefabs], cardHolder[i].transform);
                    cardActionObj.name = $"CARD_{numberCard}";

                    cardActionObj.GetComponent<DraggableObject>().Init(canvas);

                    var cardAction = cardActionObj.GetComponent<CardAction>();
                    cardAction.SetupAction(() => action(cardActionObj.name));
                    cardAction.UIView(data[i]);

                    numberCard++;
                    numberPrefabs = 0;
                }
            }
        }
    }
}
