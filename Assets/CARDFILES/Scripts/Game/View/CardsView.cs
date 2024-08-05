using Game.Component;
using Game.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public class CardsView : MonoBehaviour
    {
        private const int COUNT_CARDS = 8;
        private int numberPrefabs = 0;
        private int numberCard = 0;

        [SerializeField] private Canvas canvas;

        [Space, SerializeField] private List<GameObject> cardHolder = new List<GameObject>();

        [Space, SerializeField] private List<GameObject> cardActionPrefabs = new List<GameObject>();

        public void CardsObj(List<CardActionData> data)
        {
            for (int i = 0; i < COUNT_CARDS; i++)
            {
                if (numberPrefabs == 0)
                {
                    var cardAction = Instantiate(cardActionPrefabs[numberPrefabs], cardHolder[i].transform);
                    cardAction.name = $"CARD_{numberCard}";
                    cardAction.GetComponent<DraggableObject>().Init(canvas);

                    numberCard++;
                    numberPrefabs = 1;
                }
                else
                {
                    var cardAction = Instantiate(cardActionPrefabs[numberPrefabs], cardHolder[i].transform);
                    cardAction.name = $"CARD_{numberCard}";
                    cardAction.GetComponent<DraggableObject>().Init(canvas);

                    numberCard++;
                    numberPrefabs = 0;
                }
            }
        }
    }
}
