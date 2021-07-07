using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class TeleportToPlayer : MonoBehaviour
{
    public Transform vrOrigin;

    public Transform camera;

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
        var headPosition = vrOrigin.localPosition + vrOrigin.localRotation * camera.localPosition;

        // teleport to 0.4m below headPosition and 1m in front
        transform.localPosition = headPosition - new Vector3(0, 0.4f, 0) + Vector3.ProjectOnPlane(camera.forward, vrOrigin.up);
    }
}
