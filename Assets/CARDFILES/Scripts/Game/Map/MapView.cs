using UnityEngine;

using Game.Data;
using Game.View;
using System;

public class MapView : MonoBehaviour
{
    public const int RESOLUTION_PIXEL_IMAGE = 512;

    private float contentAreaWidth;
    private float contentAreaHeight;

    private float objectMinPointX;
    private float objectMaxPointX;
    private float objectMinPointY;
    private float objectMaxPointY;

    [SerializeField] private Vector4 gridChildImages = new Vector4(1, 1, 1, 1);
    [SerializeField] private RectTransform contentTransform;

    [Space, SerializeField] private RectTransform pointReference;
    [SerializeField, Range(0f, 0.01f)] private float horizontalDistanceBetweenObj = 0.005f;
    [SerializeField, Range(0f, 0.5f)] private float verticalDistanceBetweenObj = 0.1f;

    [Space, SerializeField] private RectTransform parentBackgorund;
    [SerializeField] private GameObject gridPrefab;

    [Space, SerializeField] private GameObject startInfoPrefab;
    [SerializeField] private GameObject endPartPrefab;
    [SerializeField] private GameObject eventPrefab;
    [SerializeField] private GameObject endGamePrefab;

    public RectTransform CurrentPartObject { get; private set; }

    public void Init()
    {
        contentAreaWidth = contentTransform.rect.width;
        contentAreaHeight = contentTransform.rect.height;

        objectMinPointY = pointReference.localPosition.y + verticalDistanceBetweenObj * contentAreaHeight;
    }

    private void InstallTrackedEvent(RectTransform transformObj)
    {
        CurrentPartObject = transformObj;
    }

    public event Action MapExtendedEvent;

    private RectTransform InfoView(GameObject prefab)
    {
        var transformObj = Instantiate(prefab, contentTransform).GetComponent<RectTransform>();

        var posX = pointReference.localPosition.x + transformObj.rect.width / 2 + contentAreaWidth * horizontalDistanceBetweenObj;
        var posY = objectMinPointY - contentAreaHeight * verticalDistanceBetweenObj - transformObj.rect.height / 2;

        transformObj.anchoredPosition = new Vector2(posX, posY);
        return transformObj;
    }

    public Vector4 GetSizeMap() => 
        new Vector4(gridChildImages.x, gridChildImages.y, gridChildImages.z, gridChildImages.w);

    public void StartInfoView(Company data)
    {
        var rect = InfoView(startInfoPrefab);
        InstallTrackedEvent(rect);

        rect.GetComponent<StartInfoView>().UIView(data);
    }

    public void EndPartInfoView(Company data)
    {
        var rect = InfoView(endPartPrefab);
        InstallTrackedEvent(rect);

        rect.GetComponent<PartInfoView>().UIView(data);
    }

    public void EndGameInfoView(Company data)
    {
        var rect = InfoView(endGamePrefab);
        InstallTrackedEvent(rect);

        rect.GetComponent<EndGameInfoView>().UIView(data);
    }

    public void EventView(EventData data)
    {
        var historyEvent = Instantiate(eventPrefab, contentTransform);
        var rectTransformHistoryEvent = historyEvent.GetComponent<RectTransform>();
        historyEvent.GetComponent<EventView>().UIView(data);

        var posX = pointReference.localPosition.x - rectTransformHistoryEvent.rect.width / 2 - contentAreaWidth * horizontalDistanceBetweenObj;
        var posY = objectMinPointY - contentAreaHeight * verticalDistanceBetweenObj - rectTransformHistoryEvent.rect.height / 2;

        rectTransformHistoryEvent.anchoredPosition = new Vector2(posX, posY);

        ObjectBoundaryPoints(rectTransformHistoryEvent);
    }

    public void ObjectBoundaryPoints(RectTransform transformObj)
    {
        var possiblePositionMinY = transformObj.localPosition.y - transformObj.rect.height / 2;
        if (possiblePositionMinY < objectMinPointY)
            objectMinPointY = possiblePositionMinY;

        //var possiblePositionMaxY = transformObj.localPosition.y + transformObj.rect.height / 2;
        //if (possiblePositionMaxY > objectMaxPointY)
        //    objectMaxPointY = possiblePositionMaxY;

        //var possiblePositionMinX = transformObj.localPosition.x - transformObj.rect.width / 2;
        //if (possiblePositionMinX < objectMinPointX)
        //    objectMinPointX = possiblePositionMinX;

        //var possiblePositionMaxX = transformObj.localPosition.x + transformObj.rect.width / 2;
        //if (possiblePositionMaxX > objectMaxPointX)
        //    objectMaxPointX = possiblePositionMaxX;
    }

    public void AutoexpansionMap(RectTransform transformObj)
    {
        var sizeMapMinX = gridChildImages.x;
        var sizeMapMaxX = gridChildImages.y;
        var sizeMapMinY = gridChildImages.z;
        var sizeMapMaxY = gridChildImages.w;

        // -y
        var availableAreaY = RESOLUTION_PIXEL_IMAGE * sizeMapMinY + RESOLUTION_PIXEL_IMAGE / 2;
        var distanceToEndMapMinY = availableAreaY + objectMinPointY;

        if (distanceToEndMapMinY < RESOLUTION_PIXEL_IMAGE)
        {
            var posMinY = sizeMapMinY * -RESOLUTION_PIXEL_IMAGE - 512;
            var posMinX = sizeMapMinX * -RESOLUTION_PIXEL_IMAGE;

            var sizeMapX = sizeMapMinX + sizeMapMaxX + 1;
            for (var i = 0; i < 3; i++)
            {
                InstantiateElementGrid(posMinX, posMinY);
                posMinX += RESOLUTION_PIXEL_IMAGE;
            }

            gridChildImages = new Vector4(sizeMapMinX, sizeMapMaxX, sizeMapMinY + 1, sizeMapMaxY);
            MapExtendedEvent?.Invoke();
        }

        // -x
    }

    private void InstantiateElementGrid(float posX, float posY)
    {
        var rectTransform = Instantiate(gridPrefab, parentBackgorund).GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(posX, posY);
    }
}
