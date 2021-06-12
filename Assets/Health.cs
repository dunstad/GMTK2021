using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    bool invulnerable = false;
    Camera cam;
    public Renderer spriteRenderer;

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

    public void Hurt(int amount)
    {
        if (!invulnerable)
        {
            cam = Camera.main;
            var shakeStrength = .05f + amount * .05f;
            cam.GetComponent<CameraShake>().Shake(shakeStrength, .5f);

            invulnerable = true;
            Invoke("MakeVulnerable", .5f);

            Flicker();

            health -=1;
            if (health <= 0)
            {
                Destroy(gameObject);
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

    void Flicker()
    {
        ToggleAlpha();
        Invoke("ToggleAlpha", .16f);
        Invoke("ToggleAlpha", .16f * 2);
        Invoke("ToggleAlpha", .16f * 3);
    }
}
