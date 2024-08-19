using Untils.FSM;

using Game.Data;
using Game.Component;

namespace Game.Management
{
    public class EndPart : IFSMStateModified<StateGameplay>
    {
        private readonly FSMGameplay fsm;
        private readonly HistorySO data;

        private readonly Company company;
        private readonly MapView mapView;

        private readonly DeckCard deckCard;

        public EndPart(FSMGameplay fsm, HistorySO data, Company company, MapView mapView, DeckCard deckCard) 
        {
            this.fsm = fsm;
            this.data = data;

            this.company = company;

            this.mapView = mapView;
            this.deckCard = deckCard;
        }

        public void Enter()
        {
        }

        public void Enter(int numberPart)
        {
            if (numberPart < data.Events.Count)
            {
                mapView.EndPartInfoView(company);
                fsm.EnterIn(StateGameplay.Part);
            }
            else
            {
                deckCard.Deinit();
                mapView.EndGameInfoView(company);
                UnityEngine.Debug.Log("finish");
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
