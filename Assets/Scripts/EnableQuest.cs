using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableQuest : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject fog;
    void OnTriggerEnter(Collider other)
    {
        enemy.SetActive(true);
        Destroy(fog);
        
    }
}
