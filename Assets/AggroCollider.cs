using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCollider : MonoBehaviour
{
    bool stillPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var animator = gameObject.GetComponent<Animator>();
        if ((col.tag == "Player") && !stillPlaying)
        {
            animator.Play("telegraph");
            stillPlaying = true;
            Invoke("EnableAggroCollider", 1.5f);
        }
    }

    void EnableAggroCollider()
    {
        stillPlaying = false;
    }
}
