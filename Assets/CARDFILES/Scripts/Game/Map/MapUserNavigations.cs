using UnityEngine;
using Game.System;

namespace Game.Component
{
    public class MapUserNavigations : MonoBehaviour
    {
        private Vector2 minActorOffset;
        private Vector2 maxActorOffset;

        private float moduleBorderPosX;
        private float moduleBorderPosY;

        [SerializeField] private int speedOffset = 15;
        [SerializeField] private RectTransform parentTransform;
        [SerializeField] private RectTransform contentTransform;
        
        [Space, SerializeField] private int resolutionPixelImage = 512;
        [SerializeField] private Vector2Int gridChildImages = new Vector2Int(3, 3);

        private UserInput userInput;

        private void Awake()
        {
            minActorOffset = parentTransform.anchorMin;
            maxActorOffset = parentTransform.anchorMax;

            moduleBorderPosX = (gridChildImages.x * resolutionPixelImage - GameSettings.TARGET_WIDTH_SCREEN * maxActorOffset.x) / 2;
            moduleBorderPosY = (gridChildImages.y * resolutionPixelImage - GameSettings.TARGET_HEIGHT_SCREEN * (maxActorOffset.y - minActorOffset.y)) / 2;
        }

        public MapUserNavigations Init(UserInput userInput)
        {
            this.userInput = userInput;
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
            var offset = (currentMousePos - previousMousePos) * Time.deltaTime * speedOffset;
            var corectOffset = new Vector2();
            var currentPos = contentTransform.anchoredPosition;

            if (offset.x < 0 && currentPos.x + offset.x > -moduleBorderPosX)
                corectOffset.x = offset.x;
            if (offset.x > 0 && currentPos.x + offset.x < moduleBorderPosX)
                corectOffset.x = offset.x;

            if (offset.y < 0 && currentPos.y + offset.y > -moduleBorderPosY)
                corectOffset.y = offset.y;
            if (offset.y > 0 && currentPos.y + offset.y < moduleBorderPosY)
                corectOffset.y = offset.y;

            contentTransform.anchoredPosition += corectOffset;
        }
    }
}
