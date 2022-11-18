using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
public class HUDProjector : MonoBehaviour
{
    public new Camera camera;
    public Transform target;

    [SerializeField]
    GameObject _contentsRoot;

    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null || camera == null)
        {
            _contentsRoot.SetActive(false);
            return;
        }
        RectTransform.position = camera.WorldToScreenPoint(target.position);
        if (RectTransform.position.z < 0)
        {
            _contentsRoot.SetActive(false);
            return;
        }
        if (!_contentsRoot.activeSelf)
            _contentsRoot.SetActive(true);
    }
}
