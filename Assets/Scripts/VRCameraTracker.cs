using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraTracker : MonoBehaviour
{
    [Tooltip("The actual transfrom that will be moved Ex. VROrigin")]
    public Transform target;

    [Tooltip("The actual pivot point that want to be teleported to the pointed location Ex. Camera")]
    public Transform pivot;

    void Update()
    {
        // track position
        transform.localPosition = target.localPosition + target.localRotation * pivot.localPosition;

        // only track y rotation
        transform.localRotation = Quaternion.Euler(0, pivot.localRotation.eulerAngles.y + target.localRotation.eulerAngles.y, 0);
    }

    // to set default target and pivot automatically to VROrigin and Camera 
    // source: Teleportable.cs from viu
#if UNITY_EDITOR
    void OnValidate()
    {
        if (target == null || pivot == null)
        {
            FindCameraAndCamRoot();
        }
    }

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
