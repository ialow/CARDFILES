using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Untils.FSM;
using Utils;

using Game.Data;
using Game.View;
using Game.Component;

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
        private readonly DeckCard deckCard;

        public Part(FSMGameplay fsm, Coroutines coroutines, HistorySO data, Company company, 
            MapView mapView, PartView partView, DeckCard deckCard) 
        {
            this.fsm = fsm;
            this.coroutines = coroutines;

            this.data = data;
            this.company = company;

            this.mapView = mapView;
            this.partView = partView;
            this.deckCard = deckCard;
        }

        private int numberPart = 0;

        private float timer;
        private float timerNormolized;

        private List<(float, Action)> eventsByTime = new List<(float, Action)>();
        private List<(float, Action)> unfinishedEvents = new List<(float, Action)>();

        public void Enter()
        {
            Debug.Log("part");
            company.MoneyChangedEvent += deckCard.IsUsingCard;
            company.Personnel.DistributionEmployeesEvent += deckCard.IsUsingCard;

            AddHistoryEvent();
            AddUnfinishedEvent();

            partView.NumberPartView(numberPart + 1);
            mapView.EventView(data.Events[numberPart]);
            deckCard.CardsObj(data.Events[numberPart].CardsAction, AddActionEvent, CostUsingActionCard);

            coroutines.StartCoroutine(TimerPart(/*GameSettings.TIME_PART*/10f));
        }

        public void Exit()
        {
            company.MoneyChangedEvent -= deckCard.IsUsingCard;
            company.Personnel.DistributionEmployeesEvent -= deckCard.IsUsingCard;

            //deckCard.DeleteCard();

            numberPart++;
        }

        public IEnumerator TimerPart(float durationPartInSeconds)
        {
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

            timer = 0f;
            timerNormolized = 0f;

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

        private void AddActionEvent(CardActionData data)
        {
            var typeAction = data.TypeAction;
            var durationActionNValue = data.DurationActionNValue;
            var parameters = data.ChangeParameters;

            var possibleTimeMark = timerNormolized + durationActionNValue;

            if (typeAction == TypeEvent.OnMoneyChanged)
            {
                if (possibleTimeMark - 1 <= 0)
                {
                    eventsByTime.Add((possibleTimeMark, () => {
                        company.MoneyChanged(parameters);
                        company.Personnel.CompleteTask(data.NumberEmployees);
                        }
                    ));
                }
                else
                {
                    var timeMarkNewPart = possibleTimeMark - 1;
                    unfinishedEvents.Add((timeMarkNewPart, () => {
                        company.MoneyChanged(parameters);
                        company.Personnel.CompleteTask(data.NumberEmployees);
                        }
                    ));
                }
            }
        }

        private void CostUsingActionCard(CardActionData data)
        {
            company.MoneyChanged(new List<float>() { -data.Cost });
            company.Personnel.MakeTask(data.NumberEmployees);
        }

        private void AddUnfinishedEvent()
        {
            //ТРЕБУЕТСЯ ТЕСТ!!!
            var countEvents = unfinishedEvents.Count;
            for (var i = 0; i < countEvents; i++)
            {
                eventsByTime.Add(unfinishedEvents[i]);
            }

            unfinishedEvents.Clear();
        }
    }
}
