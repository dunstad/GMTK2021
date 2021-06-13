using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCollider : MonoBehaviour
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
        var player = col.GetComponent<PlayerMovement>();
        player = player ? player : col.transform.root.GetComponentInChildren<PlayerMovement>();
        // don't hurt other enemies unless the player is riding them
        if (player)
        {
            // don't hurt attached players
            if (!player.attachedObject || (player.attachedObject != transform.parent.GetComponentInChildren<EnemyHeadCollider>().gameObject))
            {
                Hurt(col);
            }
        // hurt other enemies if the player is riding us
        } else if (transform.root.GetComponentInChildren<PlayerMovement>())
        {
            Hurt(col);
        }
    }

    void Hurt(Collider2D col)
    {
        var healthComponent = col.GetComponent<Health>();
        if (healthComponent)
        {
            var direction = col.transform.position - gameObject.transform.position;
            direction.Normalize();
            healthComponent.Hurt(1, direction);
        }
    }
}
