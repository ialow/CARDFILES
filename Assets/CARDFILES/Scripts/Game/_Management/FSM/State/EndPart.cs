using Untils.FSM;

using Game.Data;

namespace Game.Management
{
    public class EndPart : IFSMStateModified<StateGameplay>
    {
        private readonly FSMGameplay fsm;
        private readonly HistorySO data;

        private readonly Company company;
        private readonly MapView mapView;

        public EndPart(FSMGameplay fsm, HistorySO data, Company company, MapView mapView) 
        {
            this.fsm = fsm;
            this.data = data;

            this.company = company;

            this.mapView = mapView;
        }

        public void Enter()
        {
        }

        public void Enter(int numberPart)
        {
            mapView.EndPartInfoView(company);

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
