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
        sizeDeathCheck();
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
        transform.localScale += Vector3.one * GrowthRate * Time.deltaTime * amount;
        if (amount > maxSpeedForGrowingAudio) SFXManager.Instance.SetVolumeGoal(SFX.GROW, 1f);
        else SFXManager.Instance.SetVolumeGoal( SFX.GROW, amount / maxSpeedForGrowingAudio);
        radius = transform.localScale[0];
    }

    void ShrinkSize()
    {
        Vector3 localScale = transform.localScale - (Vector3.one * ShrinkingRate * Time.deltaTime);
        if (radius <= TrueMinSize)
            transform.localScale = Vector3.one * TrueMinSize;
        else
            transform.localScale = localScale;

        SFXManager.Instance.SetVolumeGoal( SFX.SHRINK, 1f);
        radius = transform.localScale[0];
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cold"))
            SFXManager.Instance.SetVolumeGoal(SFX.GROW, 0f);
    }

    private void sizeDeathCheck()
    {
        // Size based death check
        if (rtimer >= DeathDelay)
        {
            //print("death" + radius); // Add death logic here.
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