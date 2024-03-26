using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public int currQuest = 0;
    public TextMeshProUGUI infoText;
    public Transform waypointTarget;

    [Header("Quest Fogs")]
    public GameObject fog1;
    public GameObject fog2;
    public GameObject fog3;
    [Header("Quest Details UI")]

    [SerializeField] GameObject questDetails;
    [SerializeField] GameObject questDetailsScript;


    // Start is called before the first frame update
    void Start()
    {
        activateQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateQuest()
    {
        switch (currQuest)
        {
            case 0:
                waypointTarget = fog1.transform;
                GetComponent<MissionWaypoint>().target = waypointTarget;
                break;
            case 1:
                waypointTarget = fog2.transform;
                GetComponent<MissionWaypoint>().target = waypointTarget;
                break;
            case 5:
                Debug.Log("Score is 5");
                break;
            case 10:
                Debug.Log("Score is 10");
                break;
            default:
                brokenQuest();
                break;
        }
    }

    public void openQuest()
    {
        questDetails.SetActive(true);
        questDetailsScript.SetActive(true);
    }

    public void completeCurrentQuest()
    {
        currQuest += 1;
    }

    public void brokenQuest()
    {

    }
}
