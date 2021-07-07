using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.EventSystems;

public class TransformTracker : MonoBehaviour
{
    [Tooltip("The transform to track. Syncs this objects tranform with targets transform (and vice versa if updateTarget is true)")]
    public Transform target;

    [Tooltip("If targets transform should be updated when clones transform changes")]
    public bool updateTarget = true;

    private bool trackTargetEnabled = true;

    private void Start()
    {
        // if grabbed clone (this object) then dont track target, instead update target (if updateTarget is true)
        GetComponent<Draggable>().afterGrabbed.AddListener((Draggable d) => trackTargetEnabled = false);
        GetComponent<Draggable>().onDrop.AddListener((Draggable d) => trackTargetEnabled = true);
    }

    void Update()
    {
        if (trackTargetEnabled)
        {
            transform.localPosition = target.transform.localPosition;
            transform.localRotation = target.transform.localRotation;
        }
        else if (updateTarget)
        {
            target.transform.localPosition = transform.localPosition;
            target.transform.localRotation = transform.localRotation;
        }
    }
}
