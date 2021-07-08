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

    private void Awake()
    {
        ViveInput.AddPressDown(HandRole.LeftHand, ControllerButton.PadTouch, OnPadTouch);
    }

    private void OnDestroy()
    {
        ViveInput.RemovePressDown(HandRole.LeftHand, ControllerButton.PadTouch, OnPadTouch);
    }

    private void OnPadTouch()
    {
        var headPosition = target.localPosition + target.localRotation * pivot.localPosition;

        // teleport to 0.4m below headPosition and 1m in front
        transform.localPosition = headPosition - new Vector3(0, 0.4f, 0) + Vector3.ProjectOnPlane(pivot.forward, target.up);
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
