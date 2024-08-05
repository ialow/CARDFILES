using Game.Data;
using Game.View;
using UnityEngine;

public class MapView : MonoBehaviour
{
    private float contentAreaWidth;
    private float contentAreaHeight;

    private float objectMinPositionY;

    [SerializeField] private RectTransform contentTransform;

    [Space, SerializeField] private RectTransform pointReference;
    [SerializeField, Range(0f, 0.01f)] private float horizontalDistanceBetweenObj = 0.005f;
    [SerializeField, Range(0f, 0.5f)] private float verticalDistanceBetweenObj = 0.1f;

    [Space, SerializeField] private GameObject startInfoPrefab;
    [SerializeField] private GameObject endPartPrefab;
    [SerializeField] private GameObject eventPrefab;


    public void Init()
    {
        contentAreaWidth = contentTransform.rect.width;
        contentAreaHeight = contentTransform.rect.height;

        objectMinPositionY = pointReference.localPosition.y + verticalDistanceBetweenObj * contentAreaHeight;
    }

    private RectTransform InfoView(GameObject prefab)
    {
        var transformObj = Instantiate(prefab, contentTransform).GetComponent<RectTransform>();

        var posX = pointReference.localPosition.x + transformObj.rect.width / 2 + contentAreaWidth * horizontalDistanceBetweenObj;
        var posY = objectMinPositionY - contentAreaHeight * verticalDistanceBetweenObj - transformObj.rect.height / 2;

        transformObj.anchoredPosition = new Vector2(posX, posY);
        return transformObj;
    }

    public void StartInfoView(Company data)
    {
        var startInfo = InfoView(startInfoPrefab).GetComponent<StartInfoView>();
        startInfo.UIView(data);
    }

    public void EndPartInfoView(Company data)
    {
        var partInfo = InfoView(endPartPrefab).GetComponent<PartInfoView>();
        partInfo.UIView(data);
    }

    public void EventView(EventData data)
    {
        var historyEvent = Instantiate(eventPrefab, contentTransform);
        var rectTransformHistoryEvent = historyEvent.GetComponent<RectTransform>();
        historyEvent.GetComponent<EventView>().UIView(data);

        var posX = pointReference.localPosition.x - rectTransformHistoryEvent.rect.width / 2 - contentAreaWidth * horizontalDistanceBetweenObj;
        var posY = objectMinPositionY - contentAreaHeight * verticalDistanceBetweenObj - rectTransformHistoryEvent.rect.height / 2;

        rectTransformHistoryEvent.anchoredPosition = new Vector2(posX, posY);

        ObjectMinPositionY(rectTransformHistoryEvent);
    }

    public void ObjectMinPositionY(RectTransform transformObj)
    {
        var possiblePosition = transformObj.localPosition.y - transformObj.rect.height / 2;

        if (possiblePosition < objectMinPositionY)
            objectMinPositionY = possiblePosition;
    }
}
