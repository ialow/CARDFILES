using Untils.FSM;

using Game.System;
using Game.Data;
using Game.Component;
using Game.View;

namespace Game.Management
{
    public class Init : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay fsm;
        private readonly UserInput inputSystem;

        private readonly Company company;

        private readonly MapUserNavigations mapNavigations;
        private readonly MapView mapView;

        private readonly PartView partView;

        public Init(FSMGameplay fsm, UserInput inputSystem, Company company, 
            MapUserNavigations mapNavigations, MapView mapView, PartView partView)
        {
            this.fsm = fsm;
            this.inputSystem = inputSystem;

            this.company = company;

            this.mapNavigations = mapNavigations;
            this.mapView = mapView;

            this.partView = partView;
        }

        public void Enter()
        {
            Load();
            fsm.EnterIn(StateGameplay.Start);
        }

        public void Exit()
        { }

        private void Load()
        {
            partView.Init(company);
            mapView.Init();
            mapNavigations.Init(inputSystem).Active();
            inputSystem.OnGameplay();
        }
    }
}
