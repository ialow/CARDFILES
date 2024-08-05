using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Component
{
    public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas canvas;

        [SerializeField] private RectTransform thisRectTransform;
        [SerializeField] private Image thisAreaRaycast;

        public Transform parentContener;

        public void SetParent(Transform parent) => transform.SetParent(parent);

        public void Init(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public DraggableObject Active()
        {
            enabled = true;
            return this;
        }
        public DraggableObject Inactive()
        {
            enabled = false;
            return this;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentContener = transform.parent;
            SetParent(canvas.transform);
            transform.SetAsLastSibling();

            thisAreaRaycast.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            thisRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetParent(parentContener);
            transform.localPosition = Vector3.zero;

            thisAreaRaycast.raycastTarget = true;
        }
    }
}
