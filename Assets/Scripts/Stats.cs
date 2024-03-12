using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    [SerializeField] public float maxHealth;
    public float damage;
    public int projectileNumber;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(GameObject target, float damage)
    {
        target.GetComponent<Stats>().health -= damage;

        if (target.GetComponent<Stats>().health <= 0)
        {
            Destroy(target.gameObject);
        }
    }
}
