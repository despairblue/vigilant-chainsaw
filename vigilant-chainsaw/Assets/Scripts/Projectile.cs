using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float selfDestructThreshold = -10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < selfDestructThreshold)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Person"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
