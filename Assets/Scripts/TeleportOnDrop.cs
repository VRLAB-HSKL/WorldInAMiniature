using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnDrop : MonoBehaviour
{
    [Tooltip("The actual transfrom that will be moved Ex. CameraRig")]
    public Transform target;

    [Tooltip("The actual pivot point that want to be teleported to the pointed location Ex. CameraHead")]
    public Transform pivot;


    public void TeleportTarget()
    {
        var headVector = target != null && pivot != null ? Vector3.ProjectOnPlane(pivot.position - target.position, target.up) : Vector3.zero;
        //var targetRot = target != null ? target.rotation * rotateHead : rotateHead;

        target.position = transform.localPosition - headVector; ;
        // TODO orientation
    } 
}
