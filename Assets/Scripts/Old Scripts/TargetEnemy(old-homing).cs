using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TargetEnemyOld : MonoBehaviour
{
    private Transform target;
    private Transform originalTarget;
    private Rigidbody rb;
    private Stats playerStats;
    public float projectileSpeed;

    public bool initialShot = true;
    public Vector3 standard = new Vector3(0f, 0f, 0f);
    public int counter = 0;

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
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            if (initialShot)
            {
                rb.velocity = direction.normalized * projectileSpeed;
                counter += 1;

                Debug.Log(rb.velocity);

                if (counter == 130)
                {
                    initialShot = false;
                }
            }
            else
            {
                // rb.velocity = standard * projectileSpeed;
            }
        }
        else if (originalTarget != null)
        {
            Vector3 direction = target.position - transform.position;
            if (initialShot)
            {
                rb.velocity = direction.normalized * projectileSpeed;
                initialShot = false;
            }
            else
            {
                rb.velocity = standard * projectileSpeed;
            }
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
        if (target != null && ReferenceEquals(other.gameObject, target.gameObject))
        {
            Stats targetStats = target.gameObject.GetComponent<Stats>();
            // Null Propogation is not recommended
            if (targetStats != null)
            {
                Debug.Log("Dealing " + playerStats.damage + "To HP: " + targetStats.health);
                targetStats.TakeDamage(target.gameObject, playerStats.damage);
            }
            Destroy(gameObject);
        }
        else if (originalTarget != null && ReferenceEquals(other.gameObject, originalTarget.gameObject))
        {
            Stats originalTargetStats = originalTarget.gameObject.GetComponent<Stats>();
            // Null Propogation is not recommended
            if (originalTargetStats != null)
            {
                originalTargetStats.TakeDamage(target.gameObject, playerStats.damage);
            }
            Destroy(gameObject);
        }
    }
}
