using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCollider : MonoBehaviour
{
    public SpriteRenderer bodySprite;

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
            col.gameObject.GetComponent<PlayerMovement>().transform.SetParent(transform.root.GetComponentInChildren<Animator>().transform, true);
            col.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            col.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            col.gameObject.transform.localScale = new Vector3(1, 1, 1);

            transform.root.gameObject.GetComponentInChildren<HurtCollider>().gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.root.gameObject.GetComponentInChildren<Patrol>().enabled = false;
            col.gameObject.GetComponent<PlayerMovement>().attachedObject = gameObject;
            col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            col.gameObject.GetComponent<CharacterController2D>().m_Rigidbody2D = transform.root.GetComponent<Rigidbody2D>();
            StartCoroutine(ToGreen());
        }
    }

    public void EnableCollider()
    {
        transform.parent.Find("hurtbox").gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator ToGreen()
    {
        var startTime = Time.time;
        var red = new Color(.925f, .364f, .505f);
        var green = new Color(.549f, .890f, .341f);
        while (Time.time - startTime < 1)
        {
            var lerpedColor = Color.Lerp(red, green, Time.time - startTime);
            bodySprite.color = lerpedColor;
            yield return null;
        }
        bodySprite.color = green;
    }

    public IEnumerator ToRed()
    {
        var startTime = Time.time;
        var red = new Color(.925f, .364f, .505f);
        var green = new Color(.549f, .890f, .341f);
        while (Time.time - startTime < 1)
        {
            var lerpedColor = Color.Lerp(green, red, Time.time - startTime);
            bodySprite.color = lerpedColor;
            yield return null;
        }
        bodySprite.color = red;
    }
}
