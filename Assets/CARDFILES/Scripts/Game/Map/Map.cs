using UnityEngine;
using System;

using Game.System;

namespace Game.Model
{
    public class Map
    {
        private float widthVisibleMap = SystemConstants.TARGET_WIDTH_SCREEN;
        private float heightVisibleMap = SystemConstants.TARGET_HEIGHT_SCREEN;

        private MapObjects mapObjects = new MapObjects();

        public const float MAP_CHUNCK_SIZE = SystemConstants.TARGET_RESOLUTION_MAP_CHUNCK;

        public Vector4 Grid { get; private set; }

        public float DistanceToBorderRight { get; private set; }
        public float DistanceToBorderLeft { get; private set; }
        public float DistanceToBorderTop { get; private set; }
        public float DistanceToBorderBottom { get; private set; }

        public Vector3 MapPositionInPixel { get; private set; } = new Vector3();

        /// <param name="grid">right, left, top, bottom</param>
        public Map(Vector3 mapPositionInPixel, Vector4 grid)
        {
            Grid = grid;
            MapPositionInPixel = mapPositionInPixel;
        }

        public Map Setup(float widthVisibleMap, float heightVisibleMap)
        {
            this.widthVisibleMap = widthVisibleMap;
            this.heightVisibleMap = heightVisibleMap;

            mapObjects.ObjectBottomBorderDistanceChangedEvent += AutoexpansionBottomMap;

            RecalculationDistanceToBorder();

            return this;
        }

        public event Action ExpansionBottomMapEvent;

        private void SetMapPositionInPixel(Vector3 mapPosition) => MapPositionInPixel = mapPosition;

        // ДИНАМИЧЕСКИЙ РАСЧЕТ РАССТОЯНИЯ ДО ГРАНИЦ КАРТЫ
        private void RecalculationDistanceToBorder()
        {
            var halfSizeMapChunk = MAP_CHUNCK_SIZE / 2;
            var halfWidthVisibleMap = widthVisibleMap / 2;
            var halfHeightVisibleMap = heightVisibleMap / 2;

            DistanceToBorderRight = MAP_CHUNCK_SIZE * Grid.x + halfSizeMapChunk + MapPositionInPixel.x - halfWidthVisibleMap;
            DistanceToBorderLeft = MAP_CHUNCK_SIZE * Grid.y + halfSizeMapChunk - MapPositionInPixel.x - halfWidthVisibleMap;

            DistanceToBorderTop = MAP_CHUNCK_SIZE * Grid.z + halfSizeMapChunk + MapPositionInPixel.y - halfHeightVisibleMap;
            DistanceToBorderBottom = MAP_CHUNCK_SIZE * Grid.w + halfSizeMapChunk - MapPositionInPixel.y - halfHeightVisibleMap;
        }

        private void AutoexpansionBottomMap(float distanceFromExtremePointToBottom)
        {
            var countChunkBottomMap = Grid.w;
            var sizeBottomMap = countChunkBottomMap * MAP_CHUNCK_SIZE + MAP_CHUNCK_SIZE / 2;
            var distanceToBottomMap = sizeBottomMap + distanceFromExtremePointToBottom;

            if (distanceToBottomMap < MAP_CHUNCK_SIZE)
            {
                Grid = new Vector4(Grid.x, Grid.y, Grid.z, Grid.w + 1);
                ExpansionBottomMapEvent?.Invoke();
            }
        }

        public void MapOffset(Vector3 newPosition)
        {
            SetMapPositionInPixel(newPosition);
            RecalculationDistanceToBorder();
        }

        // МОЖНО НАЗВАТЬ ДИНАМИЧЕСКОЙ ... И ДОБАВИТЬ СТАТИЧЕСКУЮ.
        // НО ТОГДА НУЖНО ДОБАВИТЬ КРАЙНИЕ ТОЧКИ - СЕЙЧАС ОНИ СЧИТАЮТСЯ КАЖДЫЙ РАЗ НОВЫЕ
        public Vector3 GetOffsetPosition(Vector2 offset, bool isCorrectionEnabled = true)
        {
            if (isCorrectionEnabled)
            {
                var corectOffset = new Vector3();

                if (offset.x < 0 && DistanceToBorderRight + offset.x > 0)
                    corectOffset.x = offset.x;
                if (offset.x > 0 && DistanceToBorderLeft - offset.x > 0)
                    corectOffset.x = offset.x;

                if (offset.y < 0 && DistanceToBorderTop + offset.y > 0)
                    corectOffset.y = offset.y;
                if (offset.y > 0 && DistanceToBorderBottom - offset.y > 0)
                    corectOffset.y = offset.y;

                return MapPositionInPixel + corectOffset;
            }

            return new Vector3(MapPositionInPixel.x + offset.x, 
                MapPositionInPixel.y + offset.y, MapPositionInPixel.z);
        }

        public void AddObjectMap(RectTransform addObject)
        {
            mapObjects.AddObjectOnMap(addObject);
            Debug.Log("Я сработал и начал добавлять обьект!");
        }
    }
}
