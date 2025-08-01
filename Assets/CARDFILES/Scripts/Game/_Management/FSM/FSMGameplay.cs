using System.Collections.Generic;
using UnityEngine;

using Untils.FSM;

using Game.Component;
using Game.System;
using Game.Data;
using Game.View;

namespace Game.Management
{
    public class FSMGameplay : FSMDictionary<StateGameplay>
    {
        public FSMGameplay Init(UserInput inputSystem, PartView partView, HistorySO data, 
            MapUserNavigations mapNavigations, MapView mapView, DeckCard deckCards)
        {
            //partView
            var company = new Company(data.Company);
            var coroutines = new GameObject("[COROUTINES]").AddComponent<Utils.Coroutines>();

            States = new Dictionary<StateGameplay, IFSMState<StateGameplay>>()
            {
                [StateGameplay.Init] = new Init(this, inputSystem, company, mapNavigations, 
                mapView, partView, deckCards),

                [StateGameplay.Start] = new Start(this, company, mapView, partView),
                [StateGameplay.Part] = new Part(this, coroutines, data, company, mapView, partView, deckCards),
                [StateGameplay.EndPart] = new EndPart(this, data, company, mapView, deckCards),
            };

            return this;
        }
    }
}