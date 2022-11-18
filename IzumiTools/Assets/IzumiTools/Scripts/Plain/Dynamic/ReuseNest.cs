using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Controls frequently be instantiated / destroyed objects to only switches its activeSelf.<br/>
    /// Used for bullets, UIs, etc.
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    [System.Serializable]
    public class ReuseNest<T> where T : Component
    {
        public Transform nest;
        public T prefab;
        public System.Action<T> actionForNewGeneration;
        public System.Action<T> actionForReactivated;
        public int ActiveCount
        {
            get
            {
                int count = 0;
                foreach(Transform t in nest)
                {
                    if(t.gameObject.activeSelf)
                        ++count;
                }
                return count;
            }
        }
        public int LastActiveSiblingIndex
        {
            get
            {
                for(int i = nest.childCount - 1; i >= 0; --i)
                {
                    if (nest.GetChild(i).gameObject.activeSelf)
                        return i;
                }
                return 0;
            }
        }
        public T Get()
        {
            T returnObject;
            foreach (Transform childTf in nest)
            {
                if (childTf.gameObject.activeSelf)
                    continue;
                if((returnObject = childTf.GetComponent<T>()) != null)
                {
                    returnObject.gameObject.SetActive(true);
                    actionForReactivated?.Invoke(returnObject);
                    return returnObject;
                }
            }
            (returnObject = Object.Instantiate(prefab)).transform.SetParent(nest, false);
            actionForNewGeneration?.Invoke(returnObject);
            return returnObject;
        }
        public void InactivateAll()
        {
            nest.ActiveAllChidren(false);
        }
        public void DestroyAll()
        {
            nest.DestroyAllChildren();
        }
    }

}