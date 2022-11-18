using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IzumiTools
{
    /// <summary>
    /// Supports object drag and related events
    /// </summary>
    [DisallowMultipleComponent]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        bool rememberOffset = true;
        [SerializeField]
        bool targetIsUI = true;
        public UnityEvent onBeginDrag, onDrag, onEndDrag;

        //data
        static GameObject currentDragging;
        public static GameObject CurrentDragging => currentDragging;
        Vector2 offset;
        Vector3 toPosition(PointerEventData eventData)
        {
            if (targetIsUI)
            {
                return eventData.position;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    return hit.point;
                }
                else
                {
                    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (rememberOffset)
            {
                offset = transform.position - toPosition(eventData);
            }
            currentDragging = gameObject;
            onBeginDrag.Invoke();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = toPosition(eventData);
            if (rememberOffset)
                transform.position += (Vector3)offset;
            onDrag.Invoke();
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke();
            currentDragging = null;
        }
    }
}