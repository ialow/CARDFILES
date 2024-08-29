using UnityEngine;

using Game.Model;

namespace Game.View
{
    public class MapView : MonoBehaviour
    {
        private Map map;

        private RectTransform contentTransform;
        private Vector4 gridView = Vector4.zero;

        [SerializeField] private RectTransform referencePoint;

        [Space, SerializeField] private RectTransform grid;
        [SerializeField] private GameObject elementGridPrefab;

        [Space, SerializeField] private GameObject startInfoHistoryPrefab;
        [SerializeField] private GameObject eventPrefab;
        [SerializeField] private GameObject launchingEraPrefab;
        [SerializeField] private GameObject endEraPrefab;

        public void Init(Map map)
        {
            this.map = map;

            ExpansionMapView();
        }

        private void ExpansionMapView()
        {
            var gridMap = map.Grid;

            var countChunkRightMap = gridMap.x;
            var countChunkLeftMap = gridMap.y;
            var countChunkTopMap = gridMap.z;
            var countChunkBottomMap = gridMap.w;


            //if (gridView.x < gridMap.x)
            //{

            //}
            //if (gridView.y < gridMap.y)
            //{

            //}
            //if (gridView.z < gridMap.z)
            //{

            //}
            if (gridView.w < gridMap.w)
            {
                var posMinY = countChunkBottomMap * -Map.MAP_CHUNCK_SIZE * 2;
                var posMinX = countChunkTopMap * -Map.MAP_CHUNCK_SIZE;

                var sizeMapX = countChunkRightMap + countChunkLeftMap + 1;
                for (var i = 0; i < sizeMapX; i++)
                {
                    InstantiateElementGrid(posMinX, posMinY);
                    posMinX += Map.MAP_CHUNCK_SIZE;
                }
            }
        }

        private void InstantiateElementGrid(float posX, float posY)
        {
            var rectTransform = Instantiate(elementGridPrefab, grid).GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, posY);
            //Debug.Log(rectTransform.rect.width);
        }
    }
}