using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Vector3 offset;

    void Update()
    {
        transform.position = PlayerController.Instance.transform.position + new Vector3(offset.x, -PlayerController.Instance.gameObject.GetComponent<Sizer>().radius + offset.y, offset.z);
    }
}
