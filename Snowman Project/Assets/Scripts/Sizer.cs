using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Sizer : MonoBehaviour
{
    public float ShrinkingRate = 5f;
    public float GrowthRate = 10f;

    Rigidbody2D Rigidbody2D;
    Transform Transform;

    public float radius = 1;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hot"))
        {
            Transform.localScale -= Vector3.one * ShrinkingRate * Time.deltaTime;
            radius += -ShrinkingRate * Time.deltaTime;
        }
        else if (collision.collider.CompareTag("Cold"))
        {
            Transform.localScale += Vector3.one * GrowthRate * Time.deltaTime * Mathf.Abs(Rigidbody2D.velocity.x);
            radius += GrowthRate * Time.deltaTime * Mathf.Abs(Rigidbody2D.velocity.x);
        }
    }
}
