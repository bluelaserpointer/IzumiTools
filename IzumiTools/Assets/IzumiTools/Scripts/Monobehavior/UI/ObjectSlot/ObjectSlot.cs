using UnityEngine;
using UnityEngine.Events;

namespace IzumiTools
{
    /// <summary>
    /// Holds objects in particular angle/scale.
    /// </summary>
    public class ObjectSlot : MonoBehaviour
    {
        [Header("Parent")]
        [SerializeField]
        protected Transform arrangeParent;
        [SerializeField]
        protected bool returnToOriginalParentWhenDisband = true;
        [Header("Rotation")]
        [SerializeField]
        protected bool doArrangeRotation = true;
        [SerializeField]
        protected Quaternion arrangeLocalRotation;
        [SerializeField]
        protected bool returnToOriginalRotationWhenDisband;
        [Header("Scale")]
        [SerializeField]
        protected bool doArrangeScale = false;
        [SerializeField]
        protected Vector2 arrangeLocalScale = Vector2.one;
        [SerializeField]
        protected bool returnToOriginalScaleWhenDisband;
        public UnityEvent onSet, onClear;

        //data
        public Transform ArrangeParent => arrangeParent ?? transform;
        protected Transform oldParent;
        protected Vector3 oldLocalRotation;
        protected Vector3 oldLocalScale;

        public Transform GetTop()
        {
            return IsEmpty ? null : ArrangeParent.GetChild(ArrangeParent.childCount - 1);
        }
        public virtual bool AllowSlotSet(GameObject obj)
        {
            return true;
        }
        public virtual void SlotSet(GameObject obj)
        {
            if (!AllowSlotSet(obj))
                return;
            oldParent = obj.transform.parent;
            obj.transform.SetParent(ArrangeParent);
            DoAlignment(obj.transform);
            onSet.Invoke();
        }
        public virtual void DoAlignment(Transform childTransform)
        {
            if (!childTransform.IsChildOf(ArrangeParent))
                return;
            childTransform.position = ArrangeParent.position;
            if (doArrangeRotation)
            {
                oldLocalRotation = childTransform.localEulerAngles;
                childTransform.localRotation = arrangeLocalRotation;
            }
            if (doArrangeScale)
            {
                oldLocalScale = childTransform.localScale;
                childTransform.localScale = arrangeLocalScale;
            }
        }
        public virtual bool AllowSlotClear()
        {
            return true;
        }
        public virtual Transform DisbandTop()
        {
            Transform topTf = GetTop();
            return Disband(topTf) ? topTf : null;
        }
        public void DestroyTop()
        {
            Destroy(DisbandTop()?.gameObject);
        }
        public virtual bool Disband(Transform childTf)
        {
            if (childTf == null || !childTf.IsChildOf(ArrangeParent) || !AllowSlotClear())
                return false;
            if (returnToOriginalParentWhenDisband)
            {
                if (oldParent == null)
                {
                    Vector3 oldPos = childTf.position;
                    childTf.SetParent(null);
                    childTf.position = oldPos; //set nullParent causes position shift
                }
                else
                {
                    childTf.SetParent(oldParent);
                }
            }
            else
            {
                childTf.SetParent(ArrangeParent.GetComponentInParent<Canvas>().transform);
            }
            if (doArrangeRotation && returnToOriginalRotationWhenDisband)
            {
                childTf.localEulerAngles = oldLocalRotation;
            }
            if (doArrangeScale && returnToOriginalScaleWhenDisband)
            {
                childTf.localScale = oldLocalScale;
            }
            onClear.Invoke();
            return true;
        }

        public bool IsEmpty => ArrangeParent.childCount == 0;
    }

}