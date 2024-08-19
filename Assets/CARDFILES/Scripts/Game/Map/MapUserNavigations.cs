using UnityEngine;
using Game.System;
using System.Collections;

namespace Game.Component
{
    public class MapUserNavigations : MonoBehaviour
    {
        private Vector2 minActorOffset;
        private Vector2 maxActorOffset;

        private float borderOffsetMaxX;
        private float borderOffsetMinX;
        private float borderOffsetUp;
        private float borderOffsetDown;

        [SerializeField] private int speedOffset = 15;
        [SerializeField] private int speedFocusPoint = 2;

        [Space, SerializeField] private RectTransform parentTransform;
        [SerializeField] private RectTransform contentTransform;

        [Space, SerializeField] private RectTransform destinationPoint;

        private MapView mapView;
        private UserInput userInput;

        private void CalculateBordersOffset()
        {
            var sizeGridMap = mapView.GetSizeMap();
            var constResolution = MapView.RESOLUTION_PIXEL_IMAGE;

            var widthMapViewPixel = GameSettings.TARGET_WIDTH_SCREEN * maxActorOffset.x / 2;
            var heightMapViewPixel = GameSettings.TARGET_HEIGHT_SCREEN * (maxActorOffset.y - minActorOffset.y) / 2;

            borderOffsetMaxX = sizeGridMap.x * constResolution + (constResolution / 2)
                - widthMapViewPixel;
            borderOffsetMinX = sizeGridMap.y * constResolution + (constResolution / 2)
                - widthMapViewPixel;

            borderOffsetDown = sizeGridMap.z * constResolution + (constResolution / 2)
                - heightMapViewPixel;
            borderOffsetUp = sizeGridMap.w * constResolution + (constResolution / 2)
                - heightMapViewPixel;
        }

        public MapUserNavigations Init(UserInput userInput, MapView mapView)
        {
            this.userInput = userInput;
            this.mapView = mapView;

            minActorOffset = parentTransform.anchorMin;
            maxActorOffset = parentTransform.anchorMax;

            this.mapView = GetComponent<MapView>();
            this.mapView.MapExtendedEvent += CalculateBordersOffset;

            CalculateBordersOffset();

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
            userInput.FocusOnPointEvent += () => StartCoroutine(FocusOnPoint());
        }

        public void Inactive()
        {
            userInput.ConditionTrackingOffsetMouseEvent -= IsPosMouseInThisArea;
            userInput.OffsetMouseEvent -= PlacementOffset;
            userInput.FocusOnPointEvent -= () => FocusOnPoint();
        }

        private void PlacementOffset(Vector2 currentMousePos, Vector2 previousMousePos)
        {
            var offset = (currentMousePos - previousMousePos) * Time.deltaTime * speedOffset;
            var corectOffset = new Vector2();
            var currentPos = contentTransform.anchoredPosition;

            if (offset.x < 0 && currentPos.x + offset.x > -borderOffsetMinX)
                corectOffset.x = offset.x;
            if (offset.x > 0 && currentPos.x + offset.x < borderOffsetMaxX)
                corectOffset.x = offset.x;

            if (offset.y < 0 && currentPos.y + offset.y > -borderOffsetUp)
                corectOffset.y = offset.y;
            if (offset.y > 0 && currentPos.y + offset.y < borderOffsetDown)
                corectOffset.y = offset.y;

            contentTransform.anchoredPosition += corectOffset;
        }

        private IEnumerator FocusOnPoint()
        {
            var targetPoint = CalculationFocusWorldPoint();

            while (Vector3.Distance(contentTransform.position, targetPoint) > 20f)
            {
                contentTransform.position = Vector3.Lerp(contentTransform.position, targetPoint,
                    Time.deltaTime * speedFocusPoint);

                yield return null;
            }

            //contentTransform.position = targetPoint;
            //yield return null;
            Debug.Log("");
        }

        private Vector3 CalculationFocusWorldPoint()
        {
            var offsetPoint = new Vector3();

            var currentWorldPoint = mapView.CurrentPartObject.position;
            var destinationWorldPoint = destinationPoint.position;

            var distanceBetweenYCoordinates = destinationWorldPoint.y - currentWorldPoint.y;

            if (distanceBetweenYCoordinates >= 0)
            {
                var maxOffsetUp = 0f;
                if (borderOffsetDown - contentTransform.anchoredPosition.y >= distanceBetweenYCoordinates + 1e-3)
                    maxOffsetUp = distanceBetweenYCoordinates;
                else
                    maxOffsetUp = Mathf.Clamp(distanceBetweenYCoordinates, 0, borderOffsetDown - contentTransform.anchoredPosition.y);

                offsetPoint.y = Mathf.Clamp(distanceBetweenYCoordinates, 0, maxOffsetUp);

                //Debug.Log("+");
                //Debug.Log($"currentWorldPoint {currentWorldPoint}, destinationWorldPoint {destinationWorldPoint} " +
                //    $"=> distanceBetweenYCoordinates {distanceBetweenYCoordinates}");
                //Debug.Log($"borderOffsetUp {borderOffsetUp}, contentTransform.anchoredPosition.y {contentTransform.anchoredPosition.y} " +
                //    $"=> maxOffsetUp {maxOffsetUp}");
                //Debug.Log($"maxOffsetUp {maxOffsetUp}");
            }
            else
            {
                var maxOffsetUp = 0f;
                if (borderOffsetUp + contentTransform.anchoredPosition.y >= distanceBetweenYCoordinates + 1e-3)
                    maxOffsetUp = distanceBetweenYCoordinates;
                else
                    maxOffsetUp = Mathf.Clamp(distanceBetweenYCoordinates, -borderOffsetUp + contentTransform.anchoredPosition.y, 0);

                offsetPoint.y = Mathf.Clamp(distanceBetweenYCoordinates, maxOffsetUp, 0);

                //Debug.Log("-");
                //Debug.Log($"currentWorldPoint {currentWorldPoint}, destinationWorldPoint {destinationWorldPoint} " +
                //    $"=> distanceBetweenYCoordinates {distanceBetweenYCoordinates}");
                //Debug.Log($"borderOffsetUp {borderOffsetUp}, contentTransform.anchoredPosition.y {contentTransform.anchoredPosition.y} " +
                //    $"=> maxOffsetUp {maxOffsetUp}");
                //Debug.Log($"maxOffsetUp {maxOffsetUp}");
            }

            return contentTransform.position + offsetPoint;
        }
    }
}
