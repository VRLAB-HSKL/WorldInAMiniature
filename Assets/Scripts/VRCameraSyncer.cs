using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraSyncer : MonoBehaviour
{
    [Tooltip("The actual transfrom that will be moved Ex. CameraRig")]
    public Transform target;

    [Tooltip("The actual pivot point that want to be teleported to the pointed location Ex. CameraHead")]
    public Transform pivot;


    // Update is called once per frame
    void Update()
    {
        if (target.hasChanged || pivot.hasChanged)
        {
            var headVector = target != null && pivot != null ? pivot.position - target.position : Vector3.zero;
            transform.localPosition = target.transform.position + headVector;
            transform.localRotation = pivot.transform.rotation;

            transform.hasChanged = false;
            target.hasChanged = false;
            pivot.hasChanged = false;
        }
    }
}
