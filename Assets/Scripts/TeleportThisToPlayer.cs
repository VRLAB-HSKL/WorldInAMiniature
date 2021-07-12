using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Ermöglicht das Teleportieren des Objekts, welches dieses Skript besitzt, zum Spieler.
/// </summary>
public class TeleportThisToPlayer : MonoBehaviour
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
    /// Offset relativ zum Kopf des Spielers. Zum Beispiel -0.4f, um das Objekt 0.4 Meter unterhalb des Kopfes des Spielers zu
    /// positionieren.
    /// </summary>
    [Tooltip("Wie soll das Objekt in der Höhe versetzt werden(relativ zur Kopfhöhe des Spielers)? z. B. -0.4 für 0,4 Meter unterhalb des Spielerkopfes")]
    public float yOffset = -0.4f;

    /// <summary>
    /// Entfernung in Blickrichtung des Spielers
    /// </summary>
    [Tooltip("Wie weit soll das Objekt vor dem Spieler positioniert werden? z. B. 1 für 1 Meter vor den Spieler")]
    public float distance = 1f;

    /// <summary>
    /// Ob das Objekt rotiert werden soll, sodass die y-Rotation mit der des Spielers übereinstimmt.
    /// </summary>
    [Tooltip("Soll das Objekt mit der Blickrichtung des Spielers ausgerichtet werden?")]
    public bool enableRotate = false;

    /// <summary>
    /// Teleportiert dieses Objekt vor den Spieler. distance gibt dabei an wie weit vor den Spieler
    /// und yOffset gibt die Höhe, relativ zum Kopf an.
    /// </summary>
    public void Teleport()
    {
        if (enabled)
        {
            var headPosition = target.localPosition + target.localRotation * pivot.localPosition;

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
