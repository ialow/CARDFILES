using Untils;

using Game.Controller;
using Game.System;

namespace Game.Management
{
    public class Init : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay fsm;
        private readonly UserInputSystem userInputSystem;

        private readonly History history;

        private readonly MapController mapController;
        private readonly InventoryController inventoryController;

        public Init(FSMGameplay fsm, UserInputSystem userInputSystem, History history, 
            MapController mapController, InventoryController inventoryController)
        {
            this.fsm = fsm;
            this.userInputSystem = userInputSystem;

            this.history = history;

            this.mapController = mapController;
            this.inventoryController = inventoryController;
        }

        public void Enter()
        {
            mapController.Setup(userInputSystem);

            fsm.EnterIn(StateGameplay.Start);
        }

        public void Exit()
        { }
    }
}
