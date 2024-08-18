using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    public float jumpForce;

    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool grounded;

    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    public float particleMaxRate = 30f;
    public float particleMaxSpeed = 30f;
    public AnimationCurve particleSpeedCurve;
    public float groundedDistance = 1.1f;

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
            print(hit.collider.name);
            if (hit.collider.tag != "Player")
                grounded = true;
        }
        //if (hit.collider != null) grounded = true;
        //else grounded = false;

        inputX = Input.GetAxis("Horizontal");
        inputY = Mathf.Abs(Input.GetAxis("Jump"));
        rb.AddForce(Time.deltaTime * new Vector2(inputX * speed, 0), ForceMode2D.Force);
        if (grounded && Input.GetButton("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, inputY * jumpForce);

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

        //print(grounded);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Hot" || collision.gameObject.tag == "Cold")
    //    {
    //        grounded = true;
    //    }
    //}
    //
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Hot" || collision.gameObject.tag == "Cold")
    //    {
    //        grounded = false;
    //    }
    //}

    void SetEmission(float rate)
    {
        var emission1 = particleSystem1.emission;
        var emission2 = particleSystem2.emission;

        emission1.rateOverTime = rate;
        emission2.rateOverTime = rate;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + (-Vector3.up * GetComponent<Sizer>().radius * groundedDistance));
    //}
}
