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
    bool justDetached = false;

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
    }

    void Detach()
    {
        transform.SetParent(null, true);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        gameObject.GetComponent<CharacterController2D>().m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        justDetached = true;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        attachedObject.transform.root.GetComponentInChildren<Patrol>().enabled = true;
        attachedObject.transform.root.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        attachedObject.GetComponent<EnemyHeadCollider>().Invoke("EnableCollider", .1f);
        attachedObject = null;
    }

    void FixedUpdate()
    {
        if (justDetached)
        {
            gameObject.GetComponent<CharacterController2D>().m_Grounded = true;
            justDetached = false;
        }
        if (jump && gameObject.GetComponent<CharacterController2D>().m_Grounded)
        {
            gameObject.GetComponentInChildren<Animator>().Play("jump");
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
