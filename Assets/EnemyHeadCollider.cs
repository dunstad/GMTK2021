using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCollider : MonoBehaviour
{
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
        // attach the player to the enemy's head
        if (col.tag == "Player")
        {
            transform.parent.Find("hurtbox").gameObject.GetComponent<BoxCollider2D>().enabled = false;
            col.gameObject.GetComponent<PlayerMovement>().attachedObject = gameObject;
            var headOffset = transform.position.y - transform.parent.position.y;
            col.gameObject.GetComponent<PlayerMovement>().headOffset = headOffset;
            col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            col.gameObject.GetComponent<CharacterController2D>().m_Rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
            AttachToHead();
        }
    }

    public void EnableCollider()
    {
        transform.parent.Find("hurtbox").gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator AttachToHead()
    {
        Debug.Log("player collision");
        return null;
        // while (scale < 5 + (tickDifference * 40)) {
        //     scale *= 1.2f;
        //     transform.localScale = new Vector3(scale, scale, scale);
        //     yield return null;
        // }
        // Destroy(gameObject);
    }
}
