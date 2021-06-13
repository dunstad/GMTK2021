using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Player"))
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator Animate()
    {
        while (true) {
            transform.Rotate(new Vector3(0, 0, Mathf.Sin(Time.time) / 2));
            var newScale = .75f * ((Mathf.Sin(Time.time) / 2) + 1.25f);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            yield return null;
        }
    }
}
