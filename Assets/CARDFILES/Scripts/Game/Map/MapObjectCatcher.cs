using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Component
{
    public class MapObjectCatcher : MonoBehaviour, IDropHandler
    {
        [SerializeField] private RectTransform contentTransform;
        [SerializeField] private MapView mapView;

        public void OnDrop(PointerEventData eventData)
        {
            var dropped = eventData.pointerDrag;
            var draggableItem = dropped.GetComponent<DraggableObject>();
            var actionItem = dropped.GetComponent<CardAction>();

            draggableItem.Inactive().SetParent(contentTransform);
            actionItem.UseCard();

            mapView.ObjectBoundaryPoints(dropped.GetComponent<RectTransform>());
            mapView.AutoexpansionMap(dropped.GetComponent<RectTransform>()); //
        }
    }
}
