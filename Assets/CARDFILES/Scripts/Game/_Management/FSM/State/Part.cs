using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Untils.FSM;
using Utils;

using Game.System;
using Game.Data;
using Game.View;

namespace Game.Management
{
    public class Part : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay fsm;
        private readonly Coroutines coroutines;

        private readonly HistorySO data;
        private readonly Company company;

        private readonly MapView mapView;
        private readonly PartView partView;
        private readonly DeckCardView cardsView;

        public Part(FSMGameplay fsm, Coroutines coroutines, HistorySO data, Company company, 
            MapView mapView, PartView partView, DeckCardView cardsView) 
        {
            this.fsm = fsm;
            this.coroutines = coroutines;

            this.data = data;
            this.company = company;

            this.mapView = mapView;
            this.partView = partView;
            this.cardsView = cardsView;

            //
        }

        private int numberPart = 0;
        private List<(float, Action)> eventsByTime = new List<(float, Action)>();

        public void Enter()
        {
            AddHistoryEvent();

            partView.NumberPartView(numberPart + 1);
            mapView.EventView(data.Events[numberPart]);
            cardsView.CardsObj(data.Events[numberPart].CardsAction, AddActionEvent);

            // метод добавления action картам

            coroutines.StartCoroutine(TimerPart(/*GameSettings.TIME_PART*/5f));
        }

        public void Exit()
        {
            numberPart++;
        }

        public IEnumerator TimerPart(float durationPartInSeconds)
        {
            var timer = 0f;
            var timerNormolized = 0f;

            while (durationPartInSeconds - timer >= 1e-6)
            {
                var currentTime = Time.time;
                yield return new WaitForSeconds(0.01f);

                timer += Time.time - currentTime;
                timerNormolized = timer / durationPartInSeconds;

                partView.TimerView(timerNormolized);

                var eventsToRemove = new List<(float, Action)>();
                foreach ((float timeMark, Action action) in eventsByTime)
                {
                    if (timerNormolized >= timeMark)
                    {
                        action();
                        eventsToRemove.Add((timeMark, action));
                    }
                }

                eventsByTime.RemoveAll(x => eventsToRemove.Contains(x));
            }

            fsm.EnterIn<IFSMStateModified<StateGameplay>>(StateGameplay.EndPart, numberPart + 1);
        }

        private void AddHistoryEvent()
        {
            var typeAction = data.Events[numberPart].TypeAction;

            var timeMarksEvent = data.Events[numberPart].MarksEventsNValue;
            var countMarksEvent = timeMarksEvent.Count;

            var durationTimeInterval = 0f;

            if (typeAction == TypeEvent.OnMoneyChanged)
            {
                for (var i = 0; i < countMarksEvent; i++)
                {
                    var timeMark = timeMarksEvent[i];
                    var parameters = new List<float>(data.Events[numberPart].ParametersAsPercentage);

                    var multiplier = timeMark - durationTimeInterval;
                    for (var j = 0; j < parameters.Count; j++)
                        parameters[j] *= multiplier;
                    durationTimeInterval = timeMark;

                    eventsByTime.Add((timeMark, () => company.MoneyChanged(parameters)));
                }
            }
        }

        private void AddActionEvent(string i)
        {
            Debug.Log($"использование картый действия - {i}");
        }
    }
}
