using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCamera : MonoBehaviour
{
    public Transform TargetTransform;

    public Vector3 StickyOffset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(TargetTransform.position.x - StickyOffset.x,
                                              TargetTransform.position.y - StickyOffset.y,
                                              TargetTransform.position.z - StickyOffset.z);
    }
}
