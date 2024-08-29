using UnityEngine;

using Game.System;
using Game.Model;
using Game.View;

namespace Game.Controller
{
    public class MapController : MonoBehaviour
    {
        private Map map;

        [SerializeField] private RectTransform parentTransform;
        [SerializeField] private RectTransform contentTransform;

        [Space, SerializeField] private Vector4 gridMap = new Vector4(1, 1, 1, 1);

        [Space, SerializeField] private MapView mapView;
        [SerializeField] private MapUserNavigations mapNavigations;
        //[SerializeField] private MapObjectCatcher objectCatcher;

        public void Setup(UserInputSystem userInput)
        {
            var parentAnchorMin = parentTransform.anchorMin;
            var parentAnchorMax = parentTransform.anchorMax;

            var widthViewMap = SystemConstants.TARGET_WIDTH_SCREEN * parentAnchorMax.x;
            var heightViewMap = SystemConstants.TARGET_HEIGHT_SCREEN * (parentAnchorMax.y - parentAnchorMin.y);

            map = new Map(contentTransform.anchoredPosition, new Vector4(gridMap.x, gridMap.y, gridMap.z, gridMap.w))
                .Setup(widthViewMap, heightViewMap);

            mapView.Init(map);
            mapNavigations.Init(userInput, map);
        }

        public MapController Active()
        {
            mapView.Action();
            mapNavigations.Active();
            return this;
        }
    }
}