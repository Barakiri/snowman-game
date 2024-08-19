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
    public MusicManager musicManager;
    public float maxSizeForMusic = 3f;
    public float minSizeForMusic = 0.5f;

    [Header("SFX")]
    public SFXManager sfxManager;
    public float maxSpeedForGrowingAudio = 2f;
    bool touchingCold = false;

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
            musicManager.SetMusicLevel((radius - minSizeForMusic) / maxSizeForMusic);

        if (!touchingCold) sfxManager.SetGrowingVolume(0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
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
            touchingCold = true;
            transform.localScale += Vector3.one * GrowthRate * Time.deltaTime * Mathf.Abs(rb.velocity.x);
            sfxManager.SetGrowingVolume( Mathf.Abs(rb.velocity.x) / maxSpeedForGrowingAudio);
        }
        radius = transform.localScale[0];
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cold"))
            touchingCold = false;
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