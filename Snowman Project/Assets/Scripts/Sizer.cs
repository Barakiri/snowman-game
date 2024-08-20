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
    public float minSize = 0.5f;
    public float maxSize = 5f;

    Rigidbody2D rb;

    public float radius = 1;

    [Header("Music")]
    public float maxSizeForMusic = 3f;
    public float minSizeForMusic = 0.5f;

    [Header("SFX")]
    public float maxSpeedForGrowingAudio = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (radius <= maxSizeForMusic)
        {
            MusicManager.Instance.SetMusicLevel(radius / maxSizeForMusic);
        }
        else
            MusicManager.Instance.SetMusicLevel(1f);

        

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    ShrinkSize();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    GrowSize(200f);
        //}
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hot"))
        {
            ShrinkSize();
        }
        else if (collision.collider.CompareTag("Cold"))
        {
            GrowSize(Mathf.Abs(rb.velocity.x));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Icicle"))
        {
            if (radius >= collision.transform.parent.GetComponent<Icicle>().sizeBreakThreshold)
            {
                collision.transform.parent.GetComponent<Icicle>().Break();
            }
            else
            {
                SFXManager.Instance.PlayClip(SFX.ICICLEHARD, 1f);
            }
        }
    }

    void GrowSize(float amount)
    {
        if (amount > maxSpeedForGrowingAudio) SFXManager.Instance.SetVolumeGoal(SFX.GROW, 1f);
        else SFXManager.Instance.SetVolumeGoal( SFX.GROW, amount / maxSpeedForGrowingAudio);

        if (radius >= maxSize)
            transform.localScale = Vector3.one * maxSize;
        else
            transform.localScale += Vector3.one * GrowthRate * Time.deltaTime * amount;
        radius = transform.localScale[0];
    }

    void ShrinkSize()
    {
        Vector3 localScale = transform.localScale - (Vector3.one * ShrinkingRate * Time.deltaTime);
        if (radius <= minSize)
            transform.localScale = Vector3.one * minSize;
        else
            transform.localScale = localScale;

        SFXManager.Instance.SetVolumeGoal( SFX.SHRINK, 1f);
        radius = transform.localScale[0];
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cold"))
            SFXManager.Instance.SetVolumeGoal(SFX.GROW, 0f);
        if (collision.collider.CompareTag("Hot"))
            SFXManager.Instance.SetVolumeGoal(SFX.SHRINK, 0f);
    }
}