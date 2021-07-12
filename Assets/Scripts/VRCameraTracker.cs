using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Verfolgt die Camera des Spielers und passt die lokale Position und y-Rotation dieses Objekts entsprechend an.
/// </summary>
public class VRCameraTracker : MonoBehaviour
{
    /// <summary>
    /// Das Root-Objekt der Camera. Dieses Objekt ist das Objekt welches bewegt wird.
    /// </summary>
    [Tooltip("Root Objekt der Camera. z. B. VROrigin")]
    public Transform target;

    /// <summary>
    /// Das Camera Objekt.
    /// </summary>
    [Tooltip("Die Camera des Spielers")]
    public Transform pivot;

    /// <summary>
    /// Aktualisiere lokale Position auf die Position der Spielerkamera. Passe die Blickrichtung an die des Spielers an (y-Rotation)
    /// </summary>
    void Update()
    {
        // track x,y,z position
        transform.localPosition = target.localPosition + target.localRotation * pivot.localPosition;

        // only track y rotation
        transform.localRotation = Quaternion.Euler(0, pivot.localRotation.eulerAngles.y + target.localRotation.eulerAngles.y, 0);
    }

    // to set default target and pivot automatically to VROrigin and Camera 
    // source: Teleportable.cs from viu
#if UNITY_EDITOR
    /// <summary>
    /// Ermöglicht das Setzen von Standardwerten für target und pivot.
    /// </summary>
    void OnValidate()
    {
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
