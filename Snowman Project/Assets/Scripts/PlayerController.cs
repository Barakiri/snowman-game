using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 15f;
    public float JumpForce = 15f;

    Rigidbody2D Rigidbody2D;
    Transform Transform;
    float inputX;
    float inputY;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Mathf.Abs(Input.GetAxis("Jump"));
        Rigidbody2D.AddForce(Speed * Time.deltaTime * new Vector2(inputX * Speed, 0), ForceMode2D.Force);
        Rigidbody2D.AddForce(Speed * Time.deltaTime * new Vector2(0, inputY * JumpForce), ForceMode2D.Impulse);

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    Rigidbody2D.AddForce(Speed * Time.deltaTime * Vector2.left, ForceMode2D.Force);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    Rigidbody2D.AddForce(Speed * Time.deltaTime * Vector2.right, ForceMode2D.Force);
        //}
        //else if (Input.GetKey(KeyCode.Space))
        //{
        //    Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        //}
        Transform.localScale += Vector3.one * Rigidbody2D.velocity.x * Time.deltaTime;
    }
}
