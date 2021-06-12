#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    public bool jump = false;
    public bool inputEnabled = true;

    public GameObject? attachedObject = null;
    public float headOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                if (attachedObject)
                {
                    Detach();
                }
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            horizontalMove = 0;
        }
        // if (attachedObject)
        // {
        //     gameObject.GetComponent<Rigidbody2D>().position = attachedObject.transform.position;
        // }
    }

    // void LateUpdate()
    // {
    //     if (attachedObject)
    //     {
    //         gameObject.GetComponent<Rigidbody2D>().position = attachedObject.transform.position;
    //     }
    // }

    void Detach()
    {
        Debug.Log("detach");
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        attachedObject.transform.parent.Find("hurtbox").gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<CharacterController2D>().m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        attachedObject = null;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        if (attachedObject)
        {
            var newPosition = attachedObject.transform.parent.GetComponent<Rigidbody2D>().position;
            newPosition.y += headOffset;
            gameObject.GetComponent<Rigidbody2D>().position = newPosition;
            gameObject.GetComponent<Rigidbody2D>().velocity = attachedObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
        jump = false;
    }
}
