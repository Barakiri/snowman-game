using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using System;

public class Sizer : MonoBehaviour
{
    public float ShrinkingRate = 5f;
    public float GrowthRate = 10f;

    // Size Checks
    public float MinSize = 0.5f;
    public float TrueMinSize = 0.2f;
    public float DeathDelay = 5f;
    public float rtimer = 0f;

    Rigidbody2D rb;

    public float radius = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sizeDeathCheck();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print(collision.collider);
        if (collision.collider.CompareTag("Hot"))
        {
            Vector3 localScale = transform.localScale - (Vector3.one * ShrinkingRate * Time.deltaTime);
            if (radius <= TrueMinSize)
                transform.localScale = Vector3.one * TrueMinSize;
            else
                transform.localScale = localScale;
        }
        else if (collision.collider.CompareTag("Cold"))
        {
            transform.localScale += Vector3.one * GrowthRate * Time.deltaTime * Mathf.Abs(rb.velocity.x);
        }
        radius = transform.localScale[0];
    }

    private void sizeDeathCheck()
    {
        // Size based death check
        if (rtimer >= DeathDelay)
        {
            print("death" + radius); // Add death logic here.
            return;
        }
        if (radius > MinSize)
        {
            rtimer = 0;
        }
        else
        {
            rtimer += Time.deltaTime;
        }
    }
}