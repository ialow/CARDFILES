using System;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class EventData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [field: Space, SerializeField] public TypeEvent TypeAction { get; private set; }
        [field: SerializeField] public List<float> ParametersAsPercentage { get; private set; } = new List<float>();
        [field: SerializeField] public List<float> MarksEventsNValue { get; private set; }

        [field: Space, SerializeField] public List<CardActionData> CardsAction { get; private set; }

        [field: Space, SerializeField] public List<string> DescriptionCourseOfEvent { get; private set; }
    }
}
