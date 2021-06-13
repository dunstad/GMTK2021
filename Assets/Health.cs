using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    bool invulnerable = false;
    Camera cam;
    public Renderer spriteRenderer;
    public GameObject deathParticle;
    public GameObject[] displayHearts;

    // Start is called before the first frame update
    void Start()
    {
        var newColor = new Color(1, 1, 1, 0);
        spriteRenderer.material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeVulnerable()
    {
        invulnerable = false;
    }

    public void Hurt(int amount, Vector2 direction)
    {
        if (!invulnerable)
        {
            cam = Camera.main;

            health -=1;
            if (health <= 0)
            {
                Destroy(displayHearts[0]);
                var particleSystem = Instantiate(deathParticle);
                particleSystem.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
                cam.GetComponent<CameraFollow>().enabled = false;
                Destroy(gameObject);
            } else
            {
                var shakeStrength = .10f + amount * .05f;
                cam.GetComponent<CameraShake>().Shake(shakeStrength, .5f);

                invulnerable = true;
                Invoke("MakeVulnerable", .5f);

                Flicker();

                // knockback
                var knockbackStrength = 50 * amount;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    knockbackStrength * direction.x,
                    // why do i need to divide only the y direction???
                    (knockbackStrength / 10) * direction.y), ForceMode2D.Impulse);
                gameObject.GetComponent<PlayerMovement>().inputEnabled = false;
                Invoke("EnableInput", .1f);

                if (health == 2)
                {
                    Destroy(displayHearts[2]);
                } else
                {
                    Destroy(displayHearts[1]);
                }
            }
        }
    }

    void ToggleAlpha()
    {
        var currentColor = spriteRenderer.material.color;
        var newAlpha = currentColor.a <.5 ? 1 : 0;
        var newColor = new Color(1, 1, 1, newAlpha);
        spriteRenderer.material.color = newColor;

    }

    void EnableInput()
    {
        gameObject.GetComponent<PlayerMovement>().inputEnabled = true;
    }

    void Flicker()
    {
        ToggleAlpha();
        Invoke("ToggleAlpha", .16f);
        Invoke("ToggleAlpha", .16f * 2);
        Invoke("ToggleAlpha", .16f * 3);
    }
}
