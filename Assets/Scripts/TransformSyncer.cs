using UnityEngine;

public class TransformSyncer : MonoBehaviour
{
    [Tooltip("The transform to sync this objects tranform with (and vice versa if SyncCloneToOriginal is true)")]
    public Transform original;

    public bool SyncCloneToOriginal = true;

    // Update is called once per frame
    void Update()
    {
        if(original.hasChanged)
        {
            transform.localPosition = original.transform.localPosition;
            transform.localRotation = original.transform.localRotation;
            
            transform.hasChanged = false;
            original.hasChanged = false;
        }
        if(transform.hasChanged && SyncCloneToOriginal)
        {
            original.transform.localPosition = transform.localPosition;
            original.transform.localRotation = transform.localRotation;

            transform.hasChanged = false;
            original.hasChanged = false;
        }
    }
}
