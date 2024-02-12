using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    private Transform target;
    private Transform originalTarget;
    private Rigidbody rb;
    private Stats playerStats;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalTarget = target;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else if (originalTarget != null)
        {
            Vector3 direction = originalTarget.position - transform.position;
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(target != null && ReferenceEquals(other.gameObject, target.gameObject))
        {
            Stats targetStats = target.gameObject.GetComponent<Stats>();
            // Null Propogation is not recommended
            if(targetStats != null)
            {
                targetStats.TakeDamage(target.gameObject, playerStats.damage);
            }
            Destroy(gameObject);
        }
        else if(originalTarget != null && ReferenceEquals(other.gameObject, originalTarget.gameObject))
        {
            Stats originalTargetStats = originalTarget.gameObject.GetComponent<Stats>();
            // Null Propogation is not recommended
            if(originalTargetStats != null)
            {
                originalTargetStats.TakeDamage(target.gameObject, playerStats.damage);
            }
            Destroy(gameObject);
        }
    }
}
