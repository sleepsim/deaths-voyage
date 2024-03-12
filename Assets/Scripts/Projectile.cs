using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float gravityScale = 0.5f;
    [SerializeField] private float destroyDelay = 5f;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy the object after time delay
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    public void FireProjectile(float speed, string direction, string type)
    {
        if (direction == "right")
        {
            rb.velocity = transform.right * speed;
        }

        if (direction == "left")
        {
            rb.velocity = -transform.right * speed;
        }

        if (type == "player")
        {
            damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().damage;
        }

        if (type == "enemy")
        {
            damage = GameObject.FindGameObjectWithTag("Enemey").GetComponent<Stats>().damage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        // Get the Stats component of the object collided with
        Stats targetStats = other.gameObject.GetComponent<Stats>();

        // Check if the object has Stats component
        if (targetStats != null)
        {
            // Deal damage to the object
            targetStats.TakeDamage(other.gameObject, 10);
        }

        Debug.Log("hit" + other);
        // Destroy the projectile on collision
        Destroy(gameObject);
    }
}
