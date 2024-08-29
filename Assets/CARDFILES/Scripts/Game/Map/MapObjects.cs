using System;
using UnityEngine;

namespace Game.Model
{
    public class MapObjects
    {
        //private float DistanceFromExtremePointToTopBorderMap;
        private float? DistanceFromExtremePointToBottomBorderMap;
        //private float DistanceFromExtremePointToBottomRightMap;
        //private float DistanceFromExtremePointToBottomLeftMap;

        public event Action<float> ObjectBottomBorderDistanceChangedEvent;

        private void RecalculationDistanceExtremePoints(RectTransform objectSize)
        {
            var possibleDistanceExtremeBottomPoint = objectSize.localPosition.y - objectSize.rect.height / 2;

            if (DistanceFromExtremePointToBottomBorderMap is not null)
            {
                if (possibleDistanceExtremeBottomPoint < DistanceFromExtremePointToBottomBorderMap)
                {
                    SetDistance(ObjectBottomBorderDistanceChangedEvent, ref DistanceFromExtremePointToBottomBorderMap, possibleDistanceExtremeBottomPoint);
                }
            }
            else
            {
                SetDistance(ObjectBottomBorderDistanceChangedEvent, ref DistanceFromExtremePointToBottomBorderMap, possibleDistanceExtremeBottomPoint);
            }
        }

        private void SetDistance(Action<float> side, ref float? distance, float newDistance)
        {
            Debug.Log($"До изменения: {DistanceFromExtremePointToBottomBorderMap}");
            distance = newDistance;
            side?.Invoke((float)distance);
            Debug.Log($"После изменения: {DistanceFromExtremePointToBottomBorderMap}");
        }

        public void AddObjectOnMap(RectTransform addObject)
        {
            RecalculationDistanceExtremePoints(addObject);
        }
    }
}
