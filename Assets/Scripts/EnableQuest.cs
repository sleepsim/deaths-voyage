using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableQuest : MonoBehaviour
{
    // [SerializeField] GameObject enemy;
    // [SerializeField] GameObject fog;
    // void OnTriggerEnter(Collider other)
    // {
    //     enemy.SetActive(true);


    //     Destroy(fog);

    // }

    public GameObject questController;


    void OnTriggerEnter(Collider other)
    {

        // Complete current quest then activate next one to avoid null error
        questController.GetComponent<QuestController>().completeCurrentQuest();
        questController.GetComponent<QuestController>().activateQuest();

        // Open next quest
        questController.GetComponent<QuestController>().openQuest();

        Destroy(gameObject);
    }
}
