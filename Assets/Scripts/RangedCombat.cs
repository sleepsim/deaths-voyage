using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// [RequireComponent(typeof(PlayerController)), RequireComponent(typeof(Stats))]

public class RangedCombat : MonoBehaviour
{
    private PlayerController moveScript;
    private EnemyController enemyCont;
    private bool playerCombat;
    private Stats stats;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Ranged Attack Variables")]
    public bool performRangedAttack = true;
    public bool autoAttackToggle = false;
    private float attackInterval;
    private float nextAttackTime = 0;

    [Header("Ranged Projectile Variables")]
    public GameObject attackProjectile;
    public Transform attackSpawnPoint;
    private GameObject spawnedProjectile;

    // Start is called before the first frame update
    void Start()
    {
        // If used for player targeting
        if(transform.CompareTag("Player"))
        {
            moveScript = GetComponent<PlayerController>();
            playerCombat = true;
        }
        // If used for enemy targeting
        else if(transform.CompareTag("Enemy"))
        {
            enemyCont = GetComponent<EnemyController>();
            playerCombat = false;
        }
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);

        if(playerCombat)
        {
            targetEnemy = moveScript.targetEnemy;
        }
        else
        {
            targetEnemy = enemyCont.targetEnemy;
        }

        if(targetEnemy != null && performRangedAttack && Time.time > nextAttackTime && autoAttackToggle)
        {   
            //Add an if statement here to test range
            StartCoroutine(RangedAttackInterval());
        }
    }

    private IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;

        RangedAttack();

        yield return new WaitForSeconds(attackInterval);

        if(targetEnemy == null)
        {
            performRangedAttack = true;
        }
    }

    private void RangedAttack()
    {
        spawnedProjectile = Instantiate(attackProjectile, attackSpawnPoint.transform.position, attackSpawnPoint.transform.rotation);

        TargetEnemy targetEnemyScript = spawnedProjectile.GetComponent<TargetEnemy>();

        if(targetEnemyScript != null)
        {
            targetEnemyScript.SetTarget(targetEnemy.transform);
        }

        nextAttackTime = Time.time + attackInterval;
        performRangedAttack = true;
    }
}
