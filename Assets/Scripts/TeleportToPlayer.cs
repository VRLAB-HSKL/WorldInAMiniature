using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class TeleportToPlayer : MonoBehaviour
{
    [Tooltip("Camera root Ex. VROrigin")]
    public Transform target;

    [Tooltip("The actual camera Ex. Camera")]
    public Transform pivot;

    [Tooltip("Height offset from head, Ex. -0.4 to position 0.4m below head")]
    public float yOffset = -0.4f;

    [Tooltip("How far to position object in front, Ex. 1m ot position 1m in front")]
    public float distance = 1f;

    [Tooltip("If teleported object should be rotated to match players look direction")]
    public bool enableRotate = false;

    public void Teleport()
    {
        if (enabled)
        {
            var headPosition = target.localPosition + target.localRotation * pivot.localPosition;

            // teleport to 0.4m below headPosition and 1m in front
            transform.localPosition = headPosition + new Vector3(0, yOffset, 0) + distance * Vector3.ProjectOnPlane(pivot.forward, target.up);

            if (enableRotate)
            {
                transform.LookAt(transform.position + Vector3.ProjectOnPlane(pivot.forward, target.up));
            }
        }
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
