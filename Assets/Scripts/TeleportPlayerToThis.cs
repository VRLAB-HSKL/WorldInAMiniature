using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ermöglicht es den Spieler an die selbe lokale Position zu teleportieren, wie das Objekt, welches dieses Skript als
/// Komponente besitzt.
/// </summary>
public class TeleportPlayerToThis : MonoBehaviour
{
    /// <summary>
    /// Das Root-Objekt der Camera. Dieses Objekt ist das Objekt, welches bewegt wird.
    /// </summary>
    [Tooltip("Das eigentliche Objekt, welches bewegt wird. z. B. VROrigin (Root Objekt der Camera)")]
    public Transform target;

    /// <summary>
    /// Das Camera Objekt.
    /// </summary>
    [Tooltip("Die Camera, welche bewegt werden soll")]
    public Transform pivot;

    /// <summary>
    /// Teleportiert den Spieler zur selben lokalen Position, wie das Objekt, welches diese Komponente besitzt und 
    /// passt die Blickrichtung an. Bei der Position werden x, y und z berücksichtigt, bei der Rotation wird nur die Rotation,
    /// um die y-Achse berücksichtigt.
    /// </summary>
    public void Teleport()
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
    /// <summary>
    /// Ermöglicht das Setzen von Standardwerten für target und pivot.
    /// </summary>
    void OnValidate() {
        if (target == null || pivot == null)
        {
            FindCameraAndCamRoot();
        }
    }

    /// <summary>
    /// Zum Zurücksetzen von target und pivot auf die Standardwerte
    /// </summary>
    protected virtual void Reset()
    {
        FindCameraAndCamRoot();
    }
#endif
    /// <summary>
    /// Setzt pivot auf die Camera und target auf das root-Objekt der Camera.
    /// </summary>
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
