using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public Sizer sizer;

    void Update()
    {
        transform.position = target.position + new Vector3(offset.x, -sizer.radius, offset.z);
    }
}
