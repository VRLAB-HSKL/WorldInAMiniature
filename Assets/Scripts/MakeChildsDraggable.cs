using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dieses Skript fügt allen Kindobjekten die Draggable-Komponente hinzu.
/// </summary>
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
