using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollider : MonoBehaviour
{
    //private void Update()
    //{
    //    if (!transform.parent.GetComponent<Icicle>().canMove)
    //    {
    //        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<Icicle>().CollisionEvent();
        }
    }
}
