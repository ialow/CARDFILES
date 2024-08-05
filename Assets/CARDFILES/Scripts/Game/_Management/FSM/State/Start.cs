using Untils.FSM;

using Game.Data;
using Game.View;

namespace Game.Management
{
    public class Start : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay fsm;

        private readonly Company company;

        private readonly MapView mapView;
        private readonly PartView partView;

        public Start(FSMGameplay fsm, Company company, MapView mapView, PartView partView)
        {
            this.fsm = fsm;
            this.company = company;

            this.mapView = mapView;
            this.partView = partView;
        }

        public void Enter()
        {
            partView.StartInfoView();
            mapView.StartInfoView();
            fsm.EnterIn(StateGameplay.Part);
        }

        public void Exit()
        {
        }
    }
}
