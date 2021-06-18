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
            transform.localPosition = original.transform.position;
            transform.localRotation = original.transform.rotation;
            
            transform.hasChanged = false;
            original.hasChanged = false;
        }
        if(transform.hasChanged && SyncCloneToOriginal)
        {
            original.transform.position = transform.localPosition;
            original.transform.rotation = transform.localRotation;

            transform.hasChanged = false;
            original.hasChanged = false;
        }
    }
}
