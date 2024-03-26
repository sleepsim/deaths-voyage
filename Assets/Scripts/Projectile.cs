using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float destroyDelay = 5f;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy the object after time delay
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    public void FireProjectile(float speed, string direction)
    {
        if (direction == "right")
        {
            rb.velocity = transform.right * speed;
        }

        if (direction == "left")
        {
            rb.velocity = -transform.right * speed;
        }

    }

    public void SetDamage(float num)
    {
        damage = num;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("Player"))
        {
            return;
        }
        // Get the Stats component of the object collided with
        if (other.CompareTag("Enemy"))
        {
            StatsEnemy targetStats = other.gameObject.GetComponent<StatsEnemy>();
            // Check if the object has Stats component
            if (targetStats != null)
            {
                // Deal damage to the object
                targetStats.TakeDamage(other.gameObject, damage);
            }
        }
        else
        {
            Stats targetStats = other.gameObject.GetComponent<Stats>();
            // Check if the object has Stats component
            if (targetStats != null)
            {
                // Deal damage to the object
                targetStats.TakeDamage(other.gameObject, damage);
            }
        }

        // Debug.Log("hit: " + other + "for " + damage);
        // Destroy the projectile on collision
        Destroy(gameObject);
    }
}
