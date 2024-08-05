using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class CardActionData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [field: Space, SerializeField] public string Cost { get; private set; }
        [field: SerializeField] public string NumberEmployees { get; private set; }
        [field: SerializeField, Range(0f, 0.7f)] public float DurationAction { get; private set; }

        [field: Space, SerializeField] public TypeEvent TypeAction { get; private set; }
        [field: SerializeField] public List<float> ChangeParameters { get; private set; } = new List<float>();
    }
}
