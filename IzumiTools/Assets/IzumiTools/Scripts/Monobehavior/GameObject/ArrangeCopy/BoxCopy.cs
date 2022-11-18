using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCopy : MonoBehaviour
{
    public BoxCollider cloneObjectBox;
    public Transform parent;
    public float spanDistance;
    [Min(0)]
    public int plusX, plusY, plusZ;
    [Min(0)]
    public int minusX, minusY, minusZ;
    public bool cloneOnAwake = true;
    [Range(0.0F, 1.0F)]
    public float generateChance = 1.0F;
    // Start is called before the first frame update
    void Awake()
    {
        if (cloneOnAwake)
        {
            DoClone();
        }
    }
    public void DoClone()
    {
        float xSpan = cloneObjectBox.size.x + spanDistance;
        float ySpan = cloneObjectBox.size.y + spanDistance;
        float zSpan = cloneObjectBox.size.z + spanDistance;
        System.Random random = new System.Random();
        for (int xi = -minusX; xi <= plusX; ++xi)
        {
            for (int yi = -minusY; yi <= plusY; ++yi)
            {
                for (int zi = -minusZ; zi <= plusZ; ++zi)
                {
                    if(generateChance == 1.0f || random.NextDouble() < generateChance)
                        Instantiate(cloneObjectBox, transform.position + new Vector3(xi * xSpan, yi * ySpan, zi * zSpan), Quaternion.identity).transform.parent = parent;
                }
            }
        }
    }
}
