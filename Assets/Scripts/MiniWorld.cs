using System.Collections.Generic;
using UnityEngine;

public class MiniWorld : MonoBehaviour
{
    [Tooltip("origin of mini world, e.g. left hand")]
    public Transform origin;

    [Tooltip("offset from origin")]
    public Vector3 originOffset;

    [Tooltip("Objects to include in miniature world")]
    public List<GameObject> realObjects;

    private List<GameObject> clonedObjects = new List<GameObject>();

    void Start()
    {
        CloneRealWorld();
    }

    void Update()
    {
        AlignMiniWorldToOrigin();
    }

    private void AlignMiniWorldToOrigin()
    {
        var position = origin.transform.position + origin.transform.rotation * originOffset;
        transform.SetPositionAndRotation(position, origin.transform.rotation);
    }

    private void CloneRealWorld()
    {
        var parentTransform = transform;
        parentTransform.Translate(new Vector3(0f, 0.1f, 0f));

        foreach (GameObject realObject in realObjects)
        {
            GameObject clonedObject = Instantiate(realObject, parentTransform);
            clonedObjects.Add(clonedObject);

            TrackTransform(realObject, clonedObject);
        }


    }

    // Adds TransformTracker component to clonedObject and each child of it recursivly
    // clonedObject tracks realObjects transform
    private void TrackTransform(GameObject realObject, GameObject clonedObject)
    {
        Debug.Assert(realObject.transform.childCount == clonedObject.transform.childCount, "Not a clone of realObject");

        for (int i = 0; i < clonedObject.transform.childCount; i++)
        {
            var transformSync = clonedObject.transform.GetChild(i).gameObject.AddComponent<TransformTracker>();
            transformSync.target = realObject.transform.GetChild(i);

            TrackTransform(realObject.transform.GetChild(i).gameObject, clonedObject.transform.GetChild(i).gameObject);
        }
    }
}
