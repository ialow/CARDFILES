using UnityEngine;

using Game.Component;
using Game.System;
using Game.Data;
using Game.View;

namespace Game.Management
{
    public class EntryPointGameplay : MonoBehaviour
    {
        [SerializeField] private UserInput inputSystem;

        [Space, SerializeField] private MapUserNavigations mapNavigations;

        [Space, SerializeField] private PartView partView;
        [SerializeField] private MapView mapView;
        [SerializeField] private DeckCard cardsView;

        [Space, SerializeField] private HistorySO data;

        public FSMGameplay FSM { get; private set; }

        private void Awake()
        {
            FSM = new FSMGameplay().Init(inputSystem, partView, data, mapNavigations, mapView, cardsView);
            FSM.EnterIn(StateGameplay.Init);
        }
    }
}