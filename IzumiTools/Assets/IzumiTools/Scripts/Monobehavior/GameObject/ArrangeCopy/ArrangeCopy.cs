using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeCopy : MonoBehaviour
{
    public GameObject cloneObject;
    public GameObject parent;
    public float spanDistance;
    [Min(0)]
    public int plusX, plusY, plusZ;
    [Min(0)]
    public int minusX, minusY, minusZ;
    public bool cloneOnAwake = true;
    // Start is called before the first frame update
    void Awake()
    {
        if(cloneOnAwake)
        {
            DoClone();
        }
    }
    public void DoClone()
    {

        for(int xi = -minusX; xi <= plusX; ++xi)
        {
            for (int yi = -minusY; yi <= plusY; ++yi)
            {
                for (int zi = -minusZ; zi <= plusZ; ++zi)
                {
                    Instantiate(cloneObject, transform.position + new Vector3(xi, yi, zi) * spanDistance, Quaternion.identity);
                }
            }
        }
    }
}
