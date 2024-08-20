using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 15f;
    public float jumpForce;

    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool grounded;
    public float groundedDistance = 1.1f;

    [Header("Movement Particles")]
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    public float particleMaxRate = 30f;
    public float particleMaxSpeed = 30f;
    public AnimationCurve particleSpeedCurve;

    [Header("Movement Sounds")]
    public SFXManager sfxManager;
    public float landAudioSoftThreshold = 0f;
    public float landAudioMediumThreshold = 2f;
    public float landAudioHardThreshold = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, GetComponent<Sizer>().radius * groundedDistance);
        grounded = false;
        foreach (RaycastHit2D hit in hits) 
        {
            if (hit.collider.tag != "Player")
                grounded = true;
        }
        //if (hit.collider != null) grounded = true;
        //else grounded = false;

        inputX = Input.GetAxis("Horizontal");
        inputY = Mathf.Abs(Input.GetAxis("Jump"));
        rb.AddForce(Time.deltaTime * new Vector2(inputX * speed, 0), ForceMode2D.Force);
        if (grounded && Input.GetButton("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, inputY * jumpForce * GetComponent<Sizer>().radius);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }

        if (grounded)
            SetEmission( Mathf.Lerp(0, particleMaxRate, particleSpeedCurve.Evaluate(Mathf.Clamp01(Mathf.Abs(rb.velocity.x) / particleMaxSpeed))) );
        else
            SetEmission(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (GetImpulse(collision) >= landAudioHardThreshold)
        {
            sfxManager.PlayClip(SFX.LANDHARD, 1f);
            //Debug.Log($"{GetImpulse(collision)} - HARD");
        }
        else if (GetImpulse(collision) >= landAudioMediumThreshold)
        {
            sfxManager.PlayClip(SFX.LANDMED, 0.75f);
            //Debug.Log($"{GetImpulse(collision)} - MED");
        }
        else if (GetImpulse(collision) >= landAudioSoftThreshold)
        {
            sfxManager.PlayClip(SFX.LANDSOFT, 0.5f);
            //Debug.Log($"{GetImpulse(collision)} - SOFT");
        }
    }

    private static float GetImpulse(Collision2D collision)
    {
        var impulse = 0f;
        for (int i = collision.contactCount - 1; i >= 0; i--)
        {
            var contact = collision.GetContact(i);
            impulse += new Vector2(contact.normalImpulse, contact.tangentImpulse).magnitude;
        }
        return impulse;
    }

    void SetEmission(float rate)
    {
        var emission1 = particleSystem1.emission;
        var emission2 = particleSystem2.emission;

        emission1.rateOverTime = rate;
        emission2.rateOverTime = rate;
    }
}
