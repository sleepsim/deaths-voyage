using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestControllerIntro : MonoBehaviour
{
    public int currQuest = 0;
    public TextMeshProUGUI infoText;
    public Transform waypointTarget;
    [Header("Quest Fogs")]
    public GameObject introFog;
    public GameObject exitFog;

    [Header("Enemies")]
    public GameObject enemy1;

    [Header("Popup Quest Details UI")]

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
                waypointTarget = introFog.transform;
                Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                infoText.text = "-Investigate the fog ahead";
                break;
            case 1:
                waypointTarget = exitFog.transform;
                Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                infoText.text = "-Escape the turoial";
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
