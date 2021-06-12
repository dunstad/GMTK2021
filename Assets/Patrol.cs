using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform groundDetection;
    Rigidbody2D rb;
    Vector2 direction = Vector2.left;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var newVelocity = new Vector2(direction.x * speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = new Vector2(direction.x * speed * Time.deltaTime, rb.velocity.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(rb.position + Vector2.down + (direction / 2), Vector2.down, distance);
        Debug.Log(groundInfo.collider);

        if (!groundInfo.collider)
        {
            if(direction == Vector2.right)
            {
                // transform.eulerAngles = new Vector3(0, -180, 0);
                direction = Vector2.left;
            } else
            {
                // transform.eulerAngles = new Vector3(0, 0, 0);
                direction = Vector2.right;
            }
        }
    }
}