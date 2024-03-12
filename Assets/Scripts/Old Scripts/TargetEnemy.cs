// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting.Antlr3.Runtime.Misc;
// using UnityEngine;

// public class TargetEnemy : MonoBehaviour
// {
//     private Rigidbody rb;
//     private Stats playerStats;
//     public float projectileSpeed;

//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
//         rb.velocity = transform.forward * projectileSpeed;
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         // Get the Stats component of the object collided with
//         Stats targetStats = other.gameObject.GetComponent<Stats>();

//         // Check if the object has Stats component
//         if (targetStats != null)
//         {
//             // Deal damage to the object
//             targetStats.TakeDamage(other.gameObject, playerStats.damage);
//         }

//         // Destroy the projectile on collision
//         Destroy(gameObject);
//     }
// }
