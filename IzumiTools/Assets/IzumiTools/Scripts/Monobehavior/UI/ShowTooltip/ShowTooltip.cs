using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IzumiTools
{
    public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public GameObject tooltip;
        public bool hideWhenClick;
        public UnityEvent showEvent;
        public UnityEvent hideEvent;
        /// <summary>
        /// 是否被鼠标悬浮
        /// </summary>
        private bool isPointerEntered;
        public bool IsPointerEntered { get { return isPointerEntered; } }
        /// <summary>
        /// 受控的ToolTip
        /// </summary>
        public GameObject Tooltip { get { return tooltip; } }
        private void Awake()
        {
            Hide();
        }
        private void OnDisable()
        {
            Hide();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetActive(isPointerEntered = true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.SetActive(isPointerEntered = false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (hideWhenClick)
            {
                Hide();
            }
        }
        public void Show(bool cond)
        {
            if (cond)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        public void Show()
        {
            if (!tooltip.activeSelf)
            {
                tooltip.SetActive(true);
                showEvent.Invoke();
            }
        }
        public void Hide()
        {
            if (tooltip.activeSelf)
            {
                tooltip.SetActive(false);
                hideEvent.Invoke();
            }
        }
    }

}