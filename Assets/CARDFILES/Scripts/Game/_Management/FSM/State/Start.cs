using Untils;

using Game.System;
using Game.Controller;

namespace Game.Management
{
    public class Start : IFSMState<StateGameplay>
    {
        private readonly UserInputSystem inputSystem;

        private readonly MapController mapController;
        private readonly InventoryController inventoryController;

        public Start(UserInputSystem inputSystem, MapController mapController, InventoryController inventoryController)
        {
            this.inputSystem = inputSystem;

            this.mapController = mapController;
            this.inventoryController = inventoryController;
        }

        public void Enter()
        {
            mapController.Active();
            
            inputSystem.OnGameplay();
        }

        public void Exit()
        {
        }
    }
}
