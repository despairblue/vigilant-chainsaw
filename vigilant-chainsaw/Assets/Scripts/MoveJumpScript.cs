using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic platformer movement and
/// jumping script
/// </summary>
public class MoveJumpScript : MonoBehaviour
{
    public float speed;

    public float jumpForce;

    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.position += new Vector3(hor, ver, 0f) * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, jumpForce, 0f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit something");
        if (collision.transform.CompareTag("Cube"))
        {
            // rigidBody2D.gravityScale = -1;
            rigidBody2D.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("Trigger something");
    //     if (collision.transform.CompareTag("Cube"))
    //     {
    //         Debug.Log("Trigger Cube");
    //         // rigidBody2D.gravityScale = -1;
    //         rigidBody2D.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
    //     }
    // }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     Debug.Log("Trigger something");
    //     if (collision.transform.CompareTag("Cube"))
    //     {
    //         Debug.Log("Trigger Cube");
    //         // rigidBody2D.gravityScale = -1;
    //         rigidBody2D.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
    //     }
    // }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger something");
        if (collision.transform.CompareTag("Cube"))
        {
            Debug.Log("Trigger Cube");
            // rigidBody2D.gravityScale = -1;
            rigidBody2D.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
        }
    }
}
