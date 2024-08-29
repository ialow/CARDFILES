using UnityEngine;

using Game.System;
using Game.Controller;
using Game.Data;

namespace Game.Management
{
    public class EntryPointGameplay : MonoBehaviour
    {
        [SerializeField] private UserInputSystem inputSystem;

        [Space, SerializeField] private MapController mapController;
        [SerializeField] private InventoryController inventoryController;

        [Space, SerializeField] private HistorySO historyDataS0;

        public FSMGameplay FSM { get; private set; }

        private void Awake()
        {
            Application.targetFrameRate = 60;

            var history = new History(historyDataS0);

            FSM = new FSMGameplay();
            FSM.AddState((StateGameplay.Init, new Init(FSM, inputSystem, history, mapController, inventoryController)))
                .AddState((StateGameplay.Start, new Start(inputSystem, mapController, inventoryController)));
            FSM.EnterIn(StateGameplay.Init);
        }
    }
}