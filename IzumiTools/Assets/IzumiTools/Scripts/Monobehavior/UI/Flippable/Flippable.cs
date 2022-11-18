using UnityEngine;

namespace IzumiTools
{
    [DisallowMultipleComponent]
    public class Flippable : MonoBehaviour
    {
        [Header("Child Objects (Nullable)")]
        public GameObject frontObjectsParent;
        public GameObject reverseObjectsParent;

        //data
        public bool IsFront { get; protected set; }
        private void Start()
        {
            IsFront = !(270 < transform.eulerAngles.y || transform.eulerAngles.y < 90);
            UpdateSprite();
        }
        void Update()
        {
            UpdateSprite();
        }
        public void UpdateSprite()
        {
            if (270 < transform.eulerAngles.y || transform.eulerAngles.y < 90)
            {
                if (!IsFront)
                {
                    IsFront = true;
                    frontObjectsParent?.SetActive(true);
                    reverseObjectsParent?.SetActive(false);
                }
            }
            else
            {
                if (IsFront)
                {
                    IsFront = false;
                    frontObjectsParent?.SetActive(false);
                    reverseObjectsParent?.SetActive(true);
                }
            }
        }
    }

}