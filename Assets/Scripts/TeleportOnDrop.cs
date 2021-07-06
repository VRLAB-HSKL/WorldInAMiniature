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
        // set new position of vr origin
        target.localPosition = transform.localPosition - target.localRotation * pivot.localPosition;

        // calculate how much to rotate to match y rotation of dragged character
        var yRotation = transform.localRotation.eulerAngles.y - pivot.localRotation.eulerAngles.y - target.localRotation.eulerAngles.y;

        // rotate head around y axis to match dragged character look direction (only y rotation, no x and z)
        target.RotateAround(new Vector3(pivot.position.x, 0, pivot.position.z), Vector3.up, yRotation);
    }
}
