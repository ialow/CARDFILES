using UnityEngine;

using Game.System;
using Game.Model;

namespace Game.Controller
{
    public class MapUserNavigations : MonoBehaviour
    {
        private Vector2 minActorOffset;
        private Vector2 maxActorOffset;

        [SerializeField] private int speedUserOffset = 15;
        [SerializeField] private int speedFocusPoint = 2;

        [Space, SerializeField] private RectTransform parentTransform;
        [SerializeField] private RectTransform contentTransform;

        [Space, SerializeField] private RectTransform destinationPoint;

        private Map map;
        private UserInputSystem userInput;

        public MapUserNavigations Init(UserInputSystem userInput, Map map)
        {
            this.userInput = userInput;
            this.map = map;

            minActorOffset = parentTransform.anchorMin;
            maxActorOffset = parentTransform.anchorMax;

            return this;
        }

        private bool IsPosMouseInThisArea(Vector2 currentMousePos)
        {
            var posMouseOnCanvas = new Vector2(
                currentMousePos.x / Screen.width, currentMousePos.y / Screen.height);

            return (minActorOffset.x <= posMouseOnCanvas.x && posMouseOnCanvas.x <= maxActorOffset.x)
                && (minActorOffset.y <= posMouseOnCanvas.y && posMouseOnCanvas.y <= maxActorOffset.y);
        }

        public void Active()
        {
            userInput.ConditionTrackingOffsetMouseEvent += IsPosMouseInThisArea;
            userInput.OffsetMouseEvent += PlacementOffset;
        }

        public void Inactive()
        {
            userInput.ConditionTrackingOffsetMouseEvent -= IsPosMouseInThisArea;
            userInput.OffsetMouseEvent -= PlacementOffset;
        }

        private void PlacementOffset(Vector2 currentMousePos, Vector2 previousMousePos)
        {
            var offset = (currentMousePos - previousMousePos) * Time.deltaTime * speedUserOffset;

            var offsetPosition = map.GetOffsetPosition(offset);
            map.MapOffset(offsetPosition);

            contentTransform.anchoredPosition = offsetPosition;
        }
    }
}
