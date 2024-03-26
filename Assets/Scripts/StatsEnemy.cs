using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemy : MonoBehaviour
{
    public float health;
    [SerializeField] public float maxHealth;
    public float damage;
    public int projectileNumber;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject upgradeUIScript;
    public GameObject questController;
    private bool hasRun = false;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(GameObject target, float damage)
    {
        target.GetComponent<StatsEnemy>().health -= damage;

        if (target.GetComponent<StatsEnemy>().health <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                if (hasRun == false)
                {
                    upgradeUI.SetActive(true);
                    upgradeUIScript.SetActive(true);

                    questController.GetComponent<QuestController>().completeCurrentQuest();
                    questController.GetComponent<QuestController>().activateQuest();

                    hasRun = true;
                }
            }
            Destroy(target.gameObject);
        }
    }


}

