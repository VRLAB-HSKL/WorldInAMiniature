using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnDrop : MonoBehaviour
{
    [Tooltip("The actual transfrom that will be moved Ex. VROrigin")]
    public Transform target;

    [Tooltip("The actual pivot point that want to be teleported to the pointed location Ex. Camera")]
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

    // to set default target and pivot automatically to VROrigin and Camera 
    // source: Teleportable.cs from viu
#if UNITY_EDITOR
    protected virtual void Reset()
    {
        FindCameraAndCamRoot();
    }
#endif
    private void FindCameraAndCamRoot()
    {
        foreach (var cam in Camera.allCameras)
        {
            if (!cam.enabled) { continue; }
#if UNITY_5_4_OR_NEWER
            // try find vr camera eye
            if (cam.stereoTargetEye != StereoTargetEyeMask.Both) { continue; }
#endif
            pivot = cam.transform;
            target = cam.transform.root == null ? cam.transform : cam.transform.root;
        }
    }
}
