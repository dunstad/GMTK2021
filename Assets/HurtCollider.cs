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
        var healthComponent = col.GetComponent<Health>();
        if (healthComponent)
        {
            healthComponent.Hurt(1);
        }
    }
}
