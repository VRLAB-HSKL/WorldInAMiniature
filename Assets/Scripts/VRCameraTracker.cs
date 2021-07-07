using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraTracker : MonoBehaviour
{
    [Tooltip("The actual transfrom that will be moved Ex. CameraRig")]
    public Transform target;

    [Tooltip("The actual pivot point that want to be teleported to the pointed location Ex. CameraHead")]
    public Transform pivot;


    // Update is called once per frame
    void Update()
    {
        // track position
        transform.localPosition = target.localPosition + target.localRotation * pivot.localPosition;

        // only track y rotation
        transform.localRotation = Quaternion.Euler(0, pivot.localRotation.eulerAngles.y + target.localRotation.eulerAngles.y, 0);
    }
}
