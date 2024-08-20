using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Icicle"))
        {
            collision.transform.parent.GetComponent<Icicle>().CollisionEvent();
        }
    }
}
