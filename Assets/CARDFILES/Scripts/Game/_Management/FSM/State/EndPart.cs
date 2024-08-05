using Untils.FSM;

using Game.Data;

namespace Game.Management
{
    public class EndPart : IFSMStateModified<StateGameplay>
    {
        private readonly FSMGameplay fsm;
        private readonly HistorySO data;

        private readonly MapView mapView;

        public EndPart(FSMGameplay fsm, HistorySO data, MapView mapView) 
        {
            this.fsm = fsm;
            this.data = data;

            this.mapView = mapView;
        }

        public void Enter()
        {
        }

        public void Enter(int numberPart)
        {
            mapView.EndPartInfoView();

            if (numberPart < data.Events.Count)
            {
                fsm.EnterIn(StateGameplay.Part);
            }
        }

        public void Update(object obj)
        {
        }

        public void Exit()
        {
        }
    }
}
