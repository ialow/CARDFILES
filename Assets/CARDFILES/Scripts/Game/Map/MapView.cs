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

    public void StartInfoView()
    {
        // данные
        var transformStartInfo = InfoView(startInfoPrefab);
    }

    public void EndPartInfoView()
    {
        // данные
        InfoView(endPartPrefab);
    }

    public void EventView()
    {
        var transformEvent = Instantiate(eventPrefab, contentTransform).GetComponent<RectTransform>();

        var posX = pointReference.localPosition.x - transformEvent.rect.width / 2 - contentAreaWidth * horizontalDistanceBetweenObj;
        var posY = objectMinPositionY - contentAreaHeight * verticalDistanceBetweenObj - transformEvent.rect.height / 2;

        transformEvent.anchoredPosition = new Vector2(posX, posY);

        ObjectMinPositionY(transformEvent);
    }

    public void ObjectMinPositionY(RectTransform transformObj)
    {
        var possiblePosition = transformObj.localPosition.y - transformObj.rect.height / 2;

        if (possiblePosition < objectMinPositionY)
            objectMinPositionY = possiblePosition;
    }
}
