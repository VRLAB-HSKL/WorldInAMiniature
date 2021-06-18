using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildsDraggable : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Draggable>();
        }
    }

}
