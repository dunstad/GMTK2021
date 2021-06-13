using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (spriteRenderer)
        {
            spriteRenderer.material.color = newColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeVulnerable()
    {
        invulnerable = false;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Hurt(int amount, Vector2 direction)
    {
        if (!invulnerable)
        {
            var isPlayer = transform.GetComponent<CharacterController2D>();
            cam = Camera.main;

            health -=1;
            if (health <= 0) // dead
            {
                var particleSystem = Instantiate(deathParticle);
                particleSystem.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
                if (isPlayer)
                {
                    Destroy(displayHearts[0]);
                    cam.GetComponent<CameraFollow>().enabled = false;
                    Invoke("Restart", 1.5f);
                    // destroying it prevents the scene from restarting
                    transform.position = new Vector3(100, 100, 100); 
                } else
                {
                    var playerMovement = transform.root.GetComponentInChildren<PlayerMovement>();
                    if (playerMovement)
                    {
                        playerMovement.Detach(true);
                    }
                    Destroy(gameObject);
                }
            } else // not dead yet
            {
                if (isPlayer)
                {
                    var shakeStrength = .10f + amount * .05f;
                    cam.GetComponent<CameraShake>().Shake(shakeStrength, .5f);

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

                invulnerable = true;
                Invoke("MakeVulnerable", .5f);

                Flicker();
            }
        }
    }

    void ToggleAlpha()
    {
        if (spriteRenderer)
        {
            var currentColor = spriteRenderer.material.color;
            var newAlpha = currentColor.a <.5 ? 1 : 0;
            var newColor = new Color(1, 1, 1, newAlpha);
            spriteRenderer.material.color = newColor;
        }

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
